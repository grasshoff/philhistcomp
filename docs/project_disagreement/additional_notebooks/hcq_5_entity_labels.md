#### PHILOSOPHY AND HISTORY OF SCIENCE WITH COMPUTATIONAL MEANS

##### PROF. DR. GERD GRAßHOFF



# 5 Entity Labels

For the previous steps see:

* "hcq_1_clean_abstracts"

Currently we can find disagreement sentences with our combination of verb filter and noun filter: V2N2. In order to reach this aim we make use of SUPPORT-verbs and EVIDENCE-nouns. These are entities V2N2 searches for to find disagreement sentences. In this notebook we are going to give labels to these entities so that they can be highlighted when displaying a disagreement sentence. Since the SUPPORT-relation between some body of evidence and a certain statement can be negated, we want also to be able to find and highlight negation expressions. This will make it more visible if two sentences show disagreement. Thus, we view negation expressions as entities and we will label them, too.

## 0 Import Libraries

Import the required libraries and the language model. 

[Besides the **`nlp`**-object `nlp` we create a second nlp-object called `nlp`. The two nlp-objects differ only in this respect that *Named Entity recognition (NER)* is not a component of `nlp`'s natural language processing pipeline whereas for `nlp` it is. We use `nlp` to filter sentences (with V2N2) and for this purpose named entity recognition is not needed. Disabling the `ner`-component can make running the code faster.

siehe früheres NB

But for labeling the entities SUPPORT-verbs, EVIDENCE-nouns and negations we need named entity recognition in the natural language processing of the sentences. Because these are the entities that shall be recognized by the computer. Thus, we need an nlp-object with a working `ner`-component in the pipeline. This is were our second instance of an nlp-object - `nlp` - comes into play.]



## 1 Load Dataframe

Load the data stored in `HCQ_sentences.json` in the **`dataframe`** `sentences_df`. (This collection of sentences encompasses 216 sentences.)



## 2 Verb Filtered Sentences

We filter the sentences in `sentences_df` by means of the verb filter developed in "hcq_3_verb_filter.ipynb" (see also "hcq_3_verb_filter.md"). The result is a list that contains the eleven remaining sentences. The list is named `verb_filtered_sentences_2`.

​          

## 3 Noun Filtered Sentences

We use the `noun_matcher_2` to filter the sentences in `verb_filtered_sentences_2`. We have developed the noun filter in "hcq_4_noun_filter.ipynb" (see also "hcq_4_noun_filter.md" for a description). The remaining nine sentences are stored in the list `noun_filtered_sentences_2`. There were 216 sentences in our original collection (`sentences_df`) of sentences. After successively applying the verb filter and the noun filter we end up with nine sentences in  `noun_filtered_sentences_2`.

I need to mention an important detail here. The sentences in `noun_filtered_sentences_2` are of datatype **`string`**; they are **not** **`Doc`**-objects! (The code commands that the `doc.text` shall be added to the list if it the `doc` meets the given criteria. This adds strings to the list.) It is important for the sentences to be strings because for labeling entities we have to process the sentences by means of the nlp-object `nlp` . An nlp-object expects a string as the argument. Passing a Doc as the argument will cause an error.



## 4 Label Entities

For labeling the entities we use `spacy`'s *Entityruler*. For further information on how to use the Entityruler see: [https://spacy.io/usage/rule-based-matching#entityruler](https://spacy.io/usage/rule-based-matching#entityruler).

At first we initialize the Entityruler and call it `ruler`:

```python
ruler = EntityRuler(nlp, validate=True)
```

Then we tell  `ruler` what to find and how to label them.

### 4.1 SUPPORT-verbs

In this subsection we set up a device that recognizes SUPPORT-verbs (which are hints that point to the SUPPORT-relation). Through named entity recognition these verbs shall be recognized and given the label `SUPP`.

*  We start by defining label patterns for the SUPPORT-verbs. Our list of patterns is named `verb_patterns`:

  ```python
  [{"label": "SUPP", "pattern": [{"POS": "VERB", "DEP": "ROOT", "LEMMA": {"IN": ['reveal', 'show', 'suggest', 'support']}}]}, 
   {"label": "SUPP", "pattern": [{"POS": "VERB", "DEP": "xcomp", "LEMMA": "confirm"}]}]
  ```

  An entity pattern is **`dictionary`** that has two keys: `label` and `pattern`. Here the value for `label` is the string `SUPP` because we want SUPPORT-verbs to be marked with the label `SUPP`. The values for the `pattern` are the search patterns we used in the `verb_matcher_2`. These define the patterns of tokens in a Doc that shall be marked with the label `SUPP`.

* `ruler.add_patterns(verb_patterns)`: We add the entity pattern `verb_patterns` to our Entityruler `ruler`.

* `nlp.add_pipe(ruler)`: We add `ruler` to the pipeline of `nlp`. This last pipeline component will find certain token patterns (i. e. SUPPORT-verbs) and will assign to them the label `SUPP`.

* `disagreement_sentences_1 = list(nlp.pipe(noun_filtered_sentences_2))`: We now convert all strings in `noun_filtered_sentences_2` into Docs. By this processing a the SUPPORT-verbs found by `ruler` are labeled with `SUPP`.  These Docs are stored in a list. We assign the list to the variable `disagreement_sentences_1`. 

### 4.2 Nouns

Up to now we can find and label SUPPORT-verbs in a sentences. We reached this by telling the Entityruler the label and the token patterns to be marked by that label. In this subsection we want to proceed in a similar way to label EVIDENCE-nouns. But instead of describing to `ruler` patterns that it should find and label we use another possibility to tell the Entityruler which parts of a sentence are to be labeled: we give phrases (which are technically strings).

But there is another difference. As with the SUPPORT-verbs we label the verbs only. In contrast I want that more than just the noun is labeled as the entity of interest. What shall be labeled as an entity is a whole phrase. For example such a phrase will be a noun added by an article, like "[t]he findings".

I could have finished this task by means of "copy & paste". But I'd like to do it through coding. (I hope that this might reveal some interesting aspects.) I want to make use of the possibility provided by `spacy` to see the *noun chunks* of a sentence. Noun chunks are slices of sentences. A noun is the syntactical center of such an object. Words that help to specify the noun further are gathered around this center. (For more information on noun chunks see: [https://spacy.io/usage/linguistic-features#dependency-parse](https://spacy.io/usage/linguistic-features#dependency-parse).) 

Noun phrases that indicate evidence or process of gaining evidence shall get the label `EVID`. As we have seen in "hcq_4_noun_filter.ipynb" (or "hcq_4_noun_filter.md", respectively) not all sentences have EVIDENCNE-nouns. In one of them there is a "[w]e" referring to scientists or scientific institution(s) that conduct research an produce evidence. Though I subsume the "[w]e" under the name EVIDENCE-nouns I will assign to it the label `SCI`. With this label I mean to mark such entities that are scientists, groups of scientists, scientific institutions or the like.

#### 4.2.1 EVIDENCE-nouns

Looking at the search patterns for `noun_matcher_2` you'll see that in most cases the EVIDENCE-nouns are the nominal subject of their respective sentence (`nsubj`). For each sentence (Doc) in `disagreement_sentences_1` the computer shall go through all noun chunks in the respective sentence. I am interested in those noun chunks of which the noun is the nominal subject of the sentence. The noun is called the root of the noun chunk. Thus, we are looking for those noun chunks of which the root is the nominal subject of its sentence; in code: `chunk.root.dep_ == "nsubj"`. If this is the case the text of chunk shall be printed on screen. Code:

```python
for doc in disagreement_sentences_1:
    for chunk in doc.noun_chunks:
        if chunk.root.dep_ == "nsubj":
            print(chunk.text)
```

This yields the following noun chunks:

```
We
This re-analysis
This systematic review and meta-analysis
These results
Interpretation Preliminary findings
Preliminary evidence
The findings
these drugs
our survey
```

We is not an EVIDENCE-noun in the strict sense, but I regard it as such. Therefor it is okay that it is among the results. But "these drugs" is not a noun chunk that shall be labeled as indicating an EVIDENCE-noun. Let's see if we can confine the results in such a way that these "these drugs" is ruled out.

`spacy` views the syntactical structure of a sentence as being tree like. The syntactical structure is represented in the form of a *dependency tree*. This tree structure defines how the tokens of a sentences are connected to each other. As we have seen a chunk has a main part - the chunk's root - to which all other parts of the chunk are connected in a certain way. In an analogous way the chunk's root is connected to another part of the sentence which has a "higher" position in the dependency tree. This next higher sentence part is called the *head* of the chunk's root; in code: `chunk.root.head`. We change the last code a bit. The computer shall additionally print the head of the chunk's root, in the lemmatized form:

```python
for doc in disagreement_sentences_1:
    for chunk in doc.noun_chunks:
        if chunk.root.dep_ == "nsubj":
            print(chunk.text, chunk.root.head.lemma_)
```

We get:

```
We be
This re-analysis reveal
This systematic review and meta-analysis show
These results support
Interpretation Preliminary findings suggest
Preliminary evidence suggest
The findings support
these drugs have
our survey show
```

Compare this to the list of (some of) our SUPPORT-verbs:

```python
['reveal', 'show', 'suggest', 'support']
```

The chunks that have an EVIDENCE-noun as their root also have a SUPPORT-verb as their head. For example the chunk "[t]he findings" (chunk root = "findings") has as its head "support". In contrast the chunk "these drugs" does not have an EVIDENCE-noun as its root and its head is no SUPPORT-verb. Here linguistic objects might reflect what is an assumption of this project: There is a SUPPORT-relation of the metalanguage that relates evidence with statements.

We add a further condition to our code block: The computer shall only display the requested noun chunks if the lemma of its head is a member of the list `['reveal', 'show', 'suggest', 'support']`; i. e. if the head is a SUPPORT-verb. Code:

```python
for doc in disagreement_sentences_1:
    for chunk in doc.noun_chunks:
        if chunk.root.dep_ == "nsubj":
            if chunk.root.head.lemma_ in ['reveal', 'show', 'suggest', 'support']:
                print(chunk.text)
```

We get:

```
This re-analysis
This systematic review and meta-analysis
These results
Interpretation Preliminary findings
Preliminary evidence
The findings
our survey
```

This time we see only noun chunks of which the root is an EVIDENCE-noun. The chunk "these drugs" is not shown any longer. (Unfortunately, we also got rid off "[w]e", but we will deal with this problem in a second.) We make use of this last code block to define the function `evidence_chunks`. This function is to be passed on a list of Doc-objects and it returns a list of label patterns. Code:

```python
def evidence_chunks(Doc_list):
    evid_chunks = []
    for doc in Doc_list:
        for chunk in doc.noun_chunks:
            if chunk.root.dep_ == "nsubj":
                if chunk.root.head.lemma_ in ['reveal', 'show', 'suggest', 'support']:
                    evid_chunks.append({"label": "EVID", "pattern": chunk.text})
                    
    return evid_chunks
```

This function creates the empty list `evid_chunks`. It goes through each Doc-object in a given list (`Doc_list`) and looks at the noun chunks in a Doc. If the noun chunk's root is the nominal subject (`nsubj`) and if the noun chunk's head is a SUPPORT-verb, then the function will add a dictionary to `evid_chunks`. The dict has two keys: `label` and `pattern` . The value for the key `label` is always `EVID`. The value of the key `pattern` is the noun chunk in form of a string.

We pass the function `evidence_chunks` on `disagreement_sentences_1`. This automatically generates a list of label patterns for the noun chunks we saw above. We call this list of label patterns `evidence_patterns`. The result of running `evidence_patterns = evidence_chunks(disagreement_sentences_1)` looks like this:

```
[{'label': 'EVID', 'pattern': 'This re-analysis'},
 {'label': 'EVID', 'pattern': 'This systematic review and meta-analysis'},
 {'label': 'EVID', 'pattern': 'These results'},
 {'label': 'EVID', 'pattern': 'Interpretation Preliminary findings'},
 {'label': 'EVID', 'pattern': 'Preliminary evidence'},
 {'label': 'EVID', 'pattern': 'The findings'},
 {'label': 'EVID', 'pattern': 'our survey'}]
```

Finally, we add `evidence_patterns` to our Entityruler. (This will cause a warning. Please ignore it for the moment.)

#### 4.2.2 Add "trial_label_pattern"

In this way we updated our list of label patterns. After that we make a new list of Doc-objects: `disagreement_sentences_2`. In the notebook "hcq_5_entity_labels.ipynb" some sentences are displayed with `displacy`. Not only the SUPPORT-verbs are highlighted. In case there occurs an EVIDENCE-noun in a sentence, then the corresponding noun chunk is highlighted, too.

As you can see in the following sentences the noun chunks are not highlighted:

```
(0) We were unable to confirm a benefit of hydroxychloroquine or chloroquine, when used alone or with a macrolide, on in-hospital outcomes for COVID-19.

(8) Chloroquine phosphate, an old drug for treatment of malaria, is shown to have apparent efficacy and acceptable safety against COVID-19 associated pneumonia in multicenter clinical trials conducted in China.
```

I want that in (8) "multicenter clinical trials" is also labeled as `EVID` thought this phrase does not show the subject of the sentence. (For some remarks on that sentence see "hcq_4_noun_filter.md", section 4 "Noun Filter: Refinements".) We define (manually) a label pattern to match and label that phrase. We call the pattern `trial_label_pattern`:

```python
trial_label_pattern = [{"label": "EVID", "pattern": "multicenter clinical trials"}]
```

Then we add `trial_label_pattern` to our Entityruler `ruler`: `ruler.add_patterns(trial_label_pattern)`

#### 4.2.3 Add "we_label_pattern"

In a similar manner we are going to proceed with sentence (0). It is the "[w]e" that i want to label. Though I often treated "[w]e" as if it is an EVIDENCE-noun, strictly speaking it is none. In itself it does not refer to evidence or processes resulting in such evidence. But it does refer to scientists or scientific institutions that do research and thereby produce evidence. I might could have assigned to "[w]e" the label `EVID`, but I decided to label it as `SCI`.

We define the label pattern `we_label_pattern`:

```python
we_label_pattern = [{"label": "SCI", "pattern": "We"}]
```

 Then we add `we_label_pattern` to `ruler`: `ruler.add_patterns(we_label_pattern)`.

After updating `ruler` in this way, we now create a new list of Doc-objects `disagreement_sentences_3`. Further entities are labeled. As you can see "[w]e" is now labeled as `SCI` and  "multicenter clinical trials" is labeled as `EVID` ("hcq_5_entity_labels.ipynb").   

### 4.3 Negations

Finally, we will enable the Entityruler to label such entities that are of type negation. Going through the remaining nine sentences I have looked for linguistic constructions that would express a negation. By such constructions it should be denied that a SUPPORT-relation obtains between evidence and a statement. In this way I manually created the label pattern `negation_patterns`:

```python
negation_patterns = [{"label": "NEG", "pattern": [{"LEMMA": {"IN": ["not", "no", "unable"]}}]}]
```

On behalf of `negation_patterns` the Entityruler is able to find and label certain negations in sentences of `noun_filtered_sentences_2`.

With `ruler.add_patterns(negation_patterns)` we add `negation_patterns` to our Entityruler `ruler`. By this we update `ruler`. After updating the ruler we create a new list of Doc-objects from our list of filtered sentences (`noun_filtered_sentences_2`): `disagreement_sentences_4`. If there occurs a defined entity in a Doc of `disagreement_sentences_4`, this entity will be labeled according to one of the label patterns we have created so far. All label patterns we created up until now will be taken into account.

You can see the remaining nine sentences with highlighted entities in "hcq_5_entity_labels.ipynb".



## 5 Separate Sentences with Regard to Negation

In this section we are going to sort and separate the Doc-objects from `disagreement_sentences_4` according to whether there is a negation in a Doc or not. Docs without a negation are stored in a list named `sents` and Docs that have a negation are stored in a list called `negated_sents`. This work is preparatory. It is supposed to set the stage for creating pairs of sentences that resemble disagreement. The idea is to take one sentence without a negation (from `sents`) and to combine it with one other sentence that has a negation (from `negated_sents`). This pair of sentences such construed might show disagreement. 

I guess proceeding in this fashion will not cover every possible way in which disagreement could be expressed. Let' s say it will only cover a subset of disagreement expressions. It will cover cases like this:

> (PRO): "The findings show that HCQ is effective against COVID-19."
>
> (CON): "The results do not show that HCQ is effective against COVID-19."

My approach will probably not cover cases, for example, like this:

> (PRO): "The findings show that HCQ is effective against COVID-19."
>
> (CON): "The results show that HCQ is useless against COVID-19."

In the second example one could argue that PRO and CON disagree with each other. But it is not obvious that there is a negation in the sentence uttered by CON. At least CON's utterance does not seem to be PRO's sentence but with a negation added to deny what PRO claims.

Separating the sentences (Docs) of `disagreement_sentences_4` is done in the following steps:

* `negation_matcher = Matcher(nlp.vocab, validate=True)`: We create the Matcher `negation_matcher` that shall find negation expressions that we are going to define.

* `negation_pattern = [{"LEMMA": {"IN": ["not", "no", "unable"]}}]`: We define a match pattern and call it `negation_pattern`. This match pattern defines the negation expressions that shall be found by the `negation_matcher`.

* `negation_matcher.add("NEGATION_ID", None, negation_pattern)`: This adds the match pattern `negation_pattern` to the Matcher `negation_matcher`.

* We make to empty list-objects: `sents` and `negated_sents`. We will store sentences (Docs) from `disagreement_sentences_4` without a negation expression - *affirmative sentences* - in `sents`. But sentences with a negation - *negated sentences* - will be stored in `negated_sents`.

* We define the function `negation_filter()`. It takes a list of Doc-objects as the argument and sorts the  Docs of that list adding them either to `sents` or to `negated_sents`. If a Doc/sentence has no negation it will be stored in `sents`. If there occurs a negation in a sentence that sentence will be stored in `negated_sents`. Code:

  ```python
  def negation_filter(sent_list):
      for doc in sent_list:
          if len(negation_matcher(doc)) > 0:
              negated_sents.append(doc)
          else:
              sents.append(doc)
  ```

  - `negation_filter(sent_list)`: The argument give the list of Docs: `sent_list`.
  - `for doc in sent_list`: The for-loop iterates over each item (Doc) in `sent_list`. For each item in `sent_list` it is checked whether if a negation occurs in it or not. That means:
    - `if len(negation_matcher(doc)) > 0`: This checks if there is in a Doc a sequence of tokens that is in accordance with the match pattern we defined by `negation_pattern`. Only if it does `negation_matcher(doc)` will create a list that contains at least one tuple. I. e. the number of items in that list is greater than zero. Or to put it simple: a negation is found in the Doc. 
      - If this is true, then Doc is added to `negated_sents` (`negated_sents.append(doc)`).
    - `else`: If there is no negation in the Doc, the list created by `negation_matcher(doc)` is empty. Thus, the number of items in that list not greater than zero. That means that the condition `len(negation_matcher(doc)) > 0` is not fulfilled. In case that this condition (stated in the `if`-clause) is not fulfilled:
      - `sents.append(doc)`: The Doc is added to the list `sents`. (This happens in case no negation is found in the Doc.) 

* `negation_filter(disagreement_sentences_4)`: After defining the function `negation_filter()` we apply it to `disagreement_sentences_4`.

You can see the sentences of `sents` and `negated_sents` with highlighted labeled entities in "hcq_5_entity.ipynb".

**sents**

There are some issues with the first item of `sents` (index number 0):

```
(0) This re-analysis reveals severe limitations in the methodology of this study, including ambiguous inclusion/exclusion of participant data and inconsistent analysis techniques, and yielded nonsignificant differences between control and treatment groups across any treatment days.
```

Formally it is correct that this sentence appears in `sents`. There are no negations in it. (Or at least there are no expressions we defined as being a negation). But when it comes to what is said by this sentence it might be more appropriate for this sentence to be in `negated_sents`. This sentence shows one type of how disagreement could be expressed: It says that another study was done inappropriately. [And, I would say, from this it might be concluded that whatever it is that is claimed by that study this claim could hardly be justified.]  (Apart from that it states that a re-analysis did not show any important difference between the treatment group and the control group.) Combined with some other sentence in `sents` (0) might express disagreement. But this type of disagreement cannot be captured by the approach I am following here. It is not a type of disagreement expression that I want to consider here.

**negated_sents**

In `negated_sents` there is a problematic sentence, too:

```
(3) Interpretation Preliminary findings suggest that the higher CQ dosage (10-day regimen) should not be recommended for COVID-19 treatment because of its potential safety hazards.
```

The first problem with (3) is that it contains a "should" (that is deontic vocabulary). I take the findings to be empirical data or connected to empirical data. Further I would say that empirical data show that certain facts obtain (or do not obtain). I read (3) as follows: Certain facts obtain and from that one concludes that something else should not be done, i. e. the higher CQ dosage should not be recommended for the treatment of COVID-19. But it is questionable if the conclusion from facts to something that should be done is allowed.

It is said in (3) that the higher CQ dosage should not be recommended for COVID-19 because it is potentially not safe. So I might rephrase (3) as follows:

```
(3') Preliminary findings suggest that the higher CQ dosage (10-day regimen) is potentially not safe.
```

I'm not sure if it is stating a matter of fact if one says that the higher CQ dosage is potentially not safe. At least "safe" seems to be a vague term. But let us set aside this problem and go on to the next. Let us assume that it is in order to conclude from certain evidence that the higher CQ dosage is potentially not safe.

The second problem obtains for (3') as well as for (3), so I will go on to describe the problem by considering (3'). I hope that I can at least give some vague hint of what that problem is. Roughly speaking, the problem is the following: It is correct that the sentence is in `negated_sents`. And it is there because of the "not" that occurs in it. But the negation (or the "not") is not were I would expect it to be. (3) might be in `negated_sents` by mere accident. I thought that a pair of sentences would show disagreement if one sentence asserts a SUPPORT-relation obtaining between evidence and a statement $s$, while the second sentence denies that a SUPPORT-relation obtains. (To illustrate it let's change the position of the "not" in (3') and call the result (3'')). The following pair of sentences would show disagreement:

> (PRO): "Preliminary findings suggest that the higher CQ dosage (10-day regimen) is potentially safe."
>
> (CON): "Preliminary findings do not suggest that the higher CQ dosage (10-day regimen) is potentially safe." [That's sentence (3'').]

In (3'') it is the "suggest" (i. e. the SUPPORT-verb) that is negated. But in (3') it is not the "suggest" that is negated - at least not directly. I would see the following pair of sentences as showing disagreement, too:

>(PRO): "Preliminary findings suggest that the higher CQ dosage (10-day regimen) is potentially safe."
>
>(CON): "Preliminary findings suggest that the higher CQ dosage (10-day regimen) is potentially not safe." [That's sentence (3').]

I think the reason why there is disagreement in both cases - between PRO and (3'') and between PRO and (3') - is, that (3') follows from (3''). So, there might no be a problem. It might not be that important where the "not" stands in the sentence. But I am still worried. Here is why. First, (3') might follow from (3''), but it does not have to be the other way round. It is questionable if (3') and (3'') are logically equivalent. For example the sentence (3'') could be a about empirical data that have nothing to do with the question whether the higher CQ dosage has safety hazards. In this case (3'') would be true. Such empirical data would not suggest that the higher CQ dosage is potentially safe. But it wouldn't suggest that the higher CQ dosage is potentially not safe, either. So (3') would be false whereas (3'') would be true. (But perhaps this counter example is faulty. One could doubt whether empirical data that does not bear on a question would count as evidence.)

The second objection is worse. Even if (3'') and (3') were logically equivalent, how should the computer know that? The way the program is coded it enables the computer to check whether a certain token - "not" - occurs in a Doc-object or not. It does not enable the computer to draw conclusions. If it stores (3) in `negated_sents` correctly, this could be nothing more than a lucky guess.