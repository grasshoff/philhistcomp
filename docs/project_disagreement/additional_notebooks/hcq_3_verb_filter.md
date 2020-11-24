---
typora-root-url: ..\assets
---

#### PHILOSOPHY AND HISTORY OF SCIENCE WITH COMPUTATIONAL MEANS

##### PROF. DR. GERD GRAßHOFF



# 3 Verb Filter

The task of this notebook is to develop a first filter - a verb filter - that enables us to pick out certain sentences from my collection of sentences that is stored in the **`dataframe`** `sentences_df`.  But the verb filter will only be a part of a set of filters that will work most effectively if all the filters are combined. An evaluation of how good the verb filter works will be part of the development process.



## 0 Import Libraries

* Import required libraries.
* `nlp = spacy.load("en_core_web_lg", disable=["ner"])`: Create **`nlp`**-object by loading the language model `en_core_web_lg`. To make the code run faster disable the pipeline component `ner` because it is currently not needed. (See "hcq_2_sentences.md")



## 1 Load Dataframe

* `sentences_df = pd.read_json("../data/HCQ_sentences.json")`: Load the known dataframe we stored in the file `HCQ_sentences.json` in the directory `data`. We store that dataframe (as usual) in the variable `sentences_df`. (See "hcq_2_sentences.ipynb" and "hcq_2_sentences.md")



## 2 Verb Filter: A first approach

In this section I want to develop a first search pattern for the verb filter. I will begin with sketching the basic idea for such a filter. Second, I apply the initial search pattern to search for certain verbs of which I will then select those that help to find certain sentences. I will call these verbs SUPPORT-verbs. These useful verbs will form an integral part of the first verb filter. Additionally I want to evaluate how good this verb filter works. At the end i shall improve the verb filter based on the results of the assessment.

### Detour: The Basic Idea

(The way in which I try to tackle the task I am going to describe was greatly influenced by Prof. Dr. Gerd Graßhoff.)

At the point we have reached so far, we have divided the abstracts of scientific articles into single sentences. The goal of this project is to have a program that can do the following: Grouping sentences taken from abstracts of scientific publications in pairs of sentences such that the pair shows disagreement. To be more specific, in this project the disagreement will be about the question, whether hydroxychloroquine (HCQ) or chloroquine (CQ) is a treatment of COVID-19. 

As an example I have taken two sentences from our sentence collection. One could argue that these two sentences form a pair that shows disagreement. While sentence PRO could be asserted by a proponent of the position that HCQ/CQ is a treatment of COVID-19, CON could be uttered by an opponent against ("contra") that position.

>(PRO) `"The findings support the hypothesis that these drugs have efficacy in the treatment of COVID-19."`(Sentence ID: pub.1127408847-21)

>(CON) `"These results do not support the use of HCQ in patients hospitalised for documented SARS-CoV-2-positive hypoxic pneumonia."` (Sentence ID: pub.1126655433-13)

I assume that not every sentence in our sentence dataframe speaks for or against the position that HCQ/CQ is a treatment for COVID-19. And thus, I think not every sentence in our collection is apt to be grouped in a pair of **disagreement sentences** (if I may call it this way). Hence we are in need of a filter. We need a means to find exactly those sentences that could form a pair of disagreement sentences.

The filter will find those sentences we are looking for by means of certain characteristics that such sentences have. But what could such characteristics be? Let's look at our example sentences PRO and CON once more. What they have in common is that they articulate a position (say an epistemic position) regarding a statement. That statement has something to do with the assertive sentence, that HCQ/CQ is (possibly) a treatment of COVID-19. Furthermore they link the statement to some kind of evidence (`The findings`, `These results`). What PRO and CON express, is that their respective statement is justified or not justified with regard to some body of evidence. The main work in this expression is done by the main verb of the sentence - let's call it **SUPPORT**-verb. It is said that some body of evidence does **support** the statement (PRO) or does not **support** the statement. To visualize it:

| Sentence type | Noun (EVID/SCI) | Negation (NEG) | Verb (SUPP) | Statement (Span)                                             |
| ------------- | --------------- | -------------- | ----------- | ------------------------------------------------------------ |
| PRO           | `The findings`  |                | `support`   | `the hypothesis that these drugs have efficacy in the treatment of COVID-19` |
| CON           | `These results` | `[do] not`     | `support`   | `the use of HCQ in patients hospitalised for documented SARS-CoV-2-positive hypoxic pneumonia` |

In terms of formal logic you could see the SUPPORT-verb as the expression for a meta-language relation. It is a relation between some evidence (whatever that may be exactly) and and assertive sentence. In PRO the SUPPORT-relation (SUPP for short) is simply stated to hold between the evidence and the statement. In CON it is denied (negated) that this relation holds.

Since SUPPORT-verbs play an important role in the sentences we are looking for, the occurrence of a SUPPORT-verb is an characteristic of such sentences. Our filter should sort out sentences if there is a not a SUPPORT-verb in it and it should retain those sentences in which there is a SUPPORT-verb. As a next step we should identify SUPPORT-verbs.

### 2.1 An Initial Search Pattern

Now we are going to develop an initial search pattern to search for SUPPORT-verbs. This is **not** a first version of the verb filter, but it is a step towards it. In order to develop the initial search pattern I will work with an example sentence that our final program should be able to find. It is sentence CON we know from the previous subsection:

>(CON) `"These results do not support the use of HCQ in patients hospitalised for documented SARS-CoV-2-positive hypoxic pneumonia."` (Sentence ID: pub.1126655433-13)

* CON undergoes *Natural Language Processing (NLP)* by calling `nlp()` on it. Thus we create a **`Doc`**-object which we call `doc`. (Creating a `Doc`-object is basically making text out a **`string`**.)

  ```python
  doc = nlp("These results do not support the use of HCQ in patients hospitalised for documented SARS-CoV-2-positive hypoxic pneumonia.")
  ```

   A `Doc` consists of tokens which are in most cases words (but not always). Through the `nlp()`-processing it is (among other things) determined for each token to which type of words it belongs (*Part of Speech (POS)*) or which its syntactical function it has within the sentence (*Dependency (DEP)*).

* We show the text(-string) and Part-of-Speech for each token in `doc`:

  ```python
  for token in doc:
      print(token.text, token.pos_)
  ```

  We get:

  ```
  These DET
  results NOUN
  do AUX
  not PART
  support VERB
  the DET
  use NOUN
  of ADP
  HCQ PROPN
  in ADP
  patients NOUN
  hospitalised VERB
  for ADP
  documented VERB
  SARS PROPN
  - PUNCT
  CoV-2-positive NOUN
  hypoxic ADJ
  pneumonia NOUN
  . PUNCT
  ```

  As you can see there are three tokens in `doc` of which the POS-tag is `VERB`. This means there three verbs in the sentence: "support", "hospitalised", "documented".
  
* Alternatively you can run the following code which creates a **`list`** of tokens that are verbs and displays the list on screen:

  ```python
  pos_verb = []
  
  for token in doc:
      if token.pos_ == "VERB":
          pos_verb.append(token.text)
          
  print(pos_verb)
  ```

  We get:

  ```
  ['support', 'hospitalised', 'documented']
  ```

If we search only search for verbs, we find more than we are looking for. For the example sentence we want to find "support" only, but in addition we find "hospitalised" and "documented", too. Let's take a look at the syntactical structure then.

+  We show the text(-string), POS-tag and dependency for each token in `doc`:

  ```python
  for token in doc:
      print(token.text, token.pos_, token.dep_)
  ```

  We get:

  ```
  These DET det
  results NOUN nsubj
  do AUX aux
  not PART neg
  support VERB ROOT
  the DET det
  use NOUN dobj
  of ADP prep
  HCQ PROPN pobj
  in ADP prep
  patients NOUN pobj
  hospitalised VERB acl
  for ADP prep
  documented VERB amod
  SARS PROPN nmod
  - PUNCT punct
  CoV-2-positive NOUN nmod
  hypoxic ADJ amod
  pneumonia NOUN pobj
  . PUNCT punct
  ```

  In contrast to 'hospitalised' or  "documented", "support" is the only verb in `doc` that is the `ROOT` of the dependency tree.

+ Alternatively you can run the following code which creates a list of tokens that are both a verb and the root of the sentence and which displays the list on screen:

  ```python
  pos_dep_verb = []
  
  for token in doc:
      if token.pos_ == "VERB":
          if token.dep_ == "ROOT":
              pos_dep_verb.append(token.text)
          
  print(pos_dep_verb)
  ```

   This gives `['support']`. If we search for tokens in `doc` (or CON respectively) that are verbs and the sentence's root we find "support" only. This is exactly what we are looking for.

We will use `spacy`'s **`Matcher`** to search for SUPPORT-verbs. The Matcher needs a search pattern to do the search. Such a pattern is defined in the following form:

```python
pattern = [{"KEY_1": "VALUE_A"}, {"KEY_2": "VALUE_2"}, ... , {"KEY_M": "VALUE_M", ... , "KEY_N": "VALUE_N"}]
```

A pattern is given as a list. That list contains **`dict`**-objects, one dictionary for each token the searched for pattern shall encompass. A dictionary has at least one key and the corresponding value. This describes the searched for characteristic of the token.
Let's create our initial search pattern stepwise:

* We give a list: `initial_search_pattern = []`.
* Since we are searching for single verbs only, we are searching for single tokens. That means there is only one dictionary in our list: `initial_search_pattern = [{}]`.
* The token has to match two characteristics which concerns the Part-of-Speech and the dependency: `initial_search_pattern = [{"POS": , "DEP": }]`.
* Namely, the token has to be a verb and the root of the sentence tree: `initial_search_pattern = [{"POS": "VERB", "DEP": "ROOT"}]`.

This will be the search pattern with which the Matcher is going to search for SUPPORT-verbs. Let's create a Matcher to test our initial search pattern on the example sentence CON (`doc`):

* `test_matcher = Matcher(nlp.vocab, validate=True)`: Create a Matcher-object called `test_matcher`. It is mandatory to pass the first argument `nlp.vocab` to give the shared vocabulary of the nlp-object to `test_matcher`. The second argument `validate=True` is optional, but it checks if the search pattern is described in a proper form.

* `test_matcher.add("TEST_ID", None, initial_search_pattern)`: This adds our search pattern `initial_search_pattern` to `test_matcher`. 

* We search the example sentence `doc` for our pattern. Running this code:

  ```python
  for match_id, start, end in test_matcher(doc):
      print(doc[start:end].text)
  ```

  We get:

  ```
  support
  ```


With the pattern `initial_search_pattern` the Matcher finds SUPPORT-verb (and nothing else).

**Detour: What does a match (=Matcher(Doc)) look like?**

Before we create a new matcher and add our initial search pattern to it in order to search the sentences for tokens that are verbs and also the root of the respective sentence, I want to shortly sketch what happened in the last code block. I hope one will then understand better what goes on in the next section. (For the following see also the corresponding part of the notebook "hcq_3_verb_filter.ipynb".) Let's look again at the code block:
```python
  for match_id, start, end in test_matcher(doc):
      print(doc[start:end].text)
```

If you apply  the `test_matcher()` on `doc` the Matcher will search for the defined pattern in the Doc-object. In `spacy`-terminology a slice of a Doc is called a **`Span`** . If there is a Span in the Doc that matches the search pattern the matcher will store it. Since we described our search pattern as a single token which is both a verb and the root of a sentence (see above) `test_matcher` will only store Spans that are tokens which are verbs and a sentence's root. But what kind of information will the matcher store in case there is a match? Running

```python
test_matcher(doc)
```

we get:

```python
[(1961263444387358288, 4, 5)]
```

We get a list that contains one single item. This item is a **`tuple`**. The first element of this kind of tuple is some kind of ID; the second element is the index number where the matched Span starts; and the third element of the tuple is the index number where the matched Span ends. Try to slice the Doc-object `doc` into a Span with the help of the values for the start index number and end index number. Running

```python
print(doc[4:5].text)
```

we get:

```python
support
```

This is the part of `doc` `test_matcher` is supposed to find. (`.text` gives the Span in form of a string.) 
The `for`-loop in the code block is iterating over all items in the list that is created by `test_matcher(doc)`. Since all items in this list are tuples, the `for`-loop iterates over all tuples in this list. This might be seen easier if we write the code block slightly different. (This yields the same result as the original code block):

```python
for (match_id, start, end) in test_matcher(doc):
    print(doc[start:end].text)
```

For each tuple (form: `(match_id, start, end)`) in the list created by calling `test_matcher()` on `doc` the program shall:  Print out on screen a Span in form of a string. This Span is described as a slice of `doc` which is defined by index numbers that mark where the slice starts and where it ends.

If a Matcher can not find a Span in a Doc-object that matches the search pattern, it will return an empty list.



### 2.2 List of SUPPORT-verbs

In this subsection we are going to make a list of SUPPORT-verbs that we can use in a search pattern to filter verbs with our verb filter. First we create a list of Doc-objects from the sentences in our dataframe. From these Doc-objects we will extract the verbs that are the root of their respective sentence. In a third step I will manually select verbs that might be of use in our verb filter. The aim is to have a first search pattern for a Matcher that serves as a verb filter.

* `doc_list = list(nlp.pipe(sentences_df["sentence"].to_list()))`: This makes a list of Doc-objects from the sentences (strings) in column `sentence` of dataframe `sentences_df`. `sentences_df["sentence"]` selects column `sentence` from the dataframe``sentences_df`. The resulting object is a `pandas`-**`Series`** (which is an object type that is similar to a dataframe). Applying the Series-**`method`** `.to_list()` to `sentences_df["sentence"]` makes a list from that Series. We then apply `nlp.pipe()` to this list to convert each string in this list into a Doc-object (in a fast way). Calling `list()` on `nlp.pipe(sentences_df["sentence"].to_list())` puts these Doc-objects into a list. We store this list in the variable `doc_list`.

* `initial_search_matcher = Matcher(nlp.vocab, validate=True)`: We create a new Matcher  (see 2.1) and call it `initial_search_matcher`.

* `initial_search_matcher.add("INITIAL_SEARCH_ID", None, initial_search_pattern)`: We add the `initial_search_pattern` (recap: `initial_search_pattern = [{"POS": "VERB", "DEP": "ROOT"}]`) to the Matcher.
  (This new matcher is much like the `test_matcher`. We could have used it instead. But in creating a new Matcher I hoped to make it clearer at which point in the project we are.)

* We define the function `def match_verb_root()`. This function takes a list of Doc-objects (as the argument) and returns a list of strings made from Spans. Each string represents the lemmatized form of verb that is at the same time the root of its respective sentence. Code:

  ```python
  def match_verb_root(Doc_list):
      lemma = set()
      
      for Doc in Doc_list:
          for match_id, start, end in initial_search_matcher(Doc):
                  
              lemma.add(Doc[start:end].lemma_)
              
      return sorted(list(lemma), key=str.lower)
  ```

  * `lemma = set()`: At the start the function creates a **`set`** (with `set()`) and assigns it to the variable `lemma`.
    Here I use a set instead of a list so that there are no duplicates in the list that is returned at the end. If there are three Doc-objects in `doc_list` that have the verb "support" (which is also the sentence's root) in it we would end up with a list that might look like this: `[..., "support", ..., "support", ..., "support", ...]`. "support" would occur three times in the list. But we want it to occur only once. Whereas in a set there is only one instance of identical items.
  *  `for Doc in Doc_list`: We make a first `for`-loop that iterates over all Doc-objects in `Doc_list`.
    For a given Doc-object:
    * `for match_id, start, end in initial_search_matcher(Doc)`: We make a second `for`-loop. `initial_search_matcher(Doc)` creates a list tuples for that given Doc. (If there are no matches, i. e. if the search pattern can't be found in the Doc-object, this list is empty.) The second `for`-loop iterates over all tuples in the list.
      For a given tuple (if there are any):
      * `lemma.add(Doc[start:end].lemma_)`: The Doc-object is sliced into a Span that matches the search pattern (`Doc[start:end]`). Of this Span we get the basic form of the word, i. e. the so called *lemma*. For example "supports" and "supported" share the same lemma, which is "support". The lemma of the matched Span is converted into a string (this is the result of `Doc[start:end].lemma_`). With the method `.add()` we add this lemma string to the set `lemma`.
    * If this is done with the first tuple, the second `for`-loop will start again with the next tuple in the list.
  * If there are no tuples left in the list for the current Doc-object, the first `for`-loop will start again with the next Doc in `Doc_list`.
  * If there are no Doc-objects left in `Doc_list`, the first loop will and the program proceeds with the next step.
  * `return sorted(list(lemma), key=str.lower)`: We convert the set `lemma` into a list using `list()`. The function `sorted()` will sort the items in that list in a certain way. The first argument of `sorted()` tells the program which list is to be sorted. The second argument `key=str.lower` tells the program to take each item in the list as a string that consists of lower case characters only. In this way the items in the list are lower case strings in alphabetical order. `return` gives such a sorted list if the function `match_verb_root()` is applied to a `Doc_list`.

* `root_verb_lemma = match_verb_root(doc_list)`: We apply the function `match_verb_root()` to `doc_list`. The resulting list is assigned to the variable `root_verb_lemma`.

Now we have a list of verbs that occur in the sentences in `sentences_df` as the root of the respective sentence. (The verbs in the list are written lower case and show the lemma form. Further the list is ordered alphabetically. There are no duplicates. You can see the list in "hcq_3_verb_filter.ipynb", subsection "2.2 List of SUPPORT-verbs".) From this list I will select manually those verbs that would be useful for the verb filter. 

**Aid to help selecting verbs manually**

To make selecting verbs by hand easier for me I have written some code. But actually this code is not necessary so feel free to skip it as well as this comment. I write these lines merely in order to record how I proceeded in selecting verbs.

The reason why we develop a verb filter is to detect sentences that might play a role in the utterance of disagreement. We want to find these disagreement sentences with the help of special verbs. I called them SUPPORT-verbs. In the current step we go this way in reverse. From the "root verbs" we select those that occur in the disagreement sentences. We aim at finding disagreement sentences by certain verbs but now we want to find these verbs by means of the disagreement sentences. This procedure is circular. But there is another criterion for selecting verbs. It is not enough that a verb is the root of a disagreement sentence. That verb should express that some body of evidence supports a statement. If it does not I will not select it.

That much for the basic idea of what I am going to do now. I'll turn to the technical implementation. But I hope to do it briefly and not in detail, because this code is not necessary for the project.

* `verb_generator = (verb for verb in root_verb_lemma)`: We create a **`generator`** from the list `root_verb_lemma` and call that generator `verb_generator`. `verb_generator` has the same items and in the same order as `root_verb_lemma`. You can call the function `next()` on a generator to get the next item in the generator. Let's say we made a generator from the list `['add', 'adjust', 'affect']`. Calling `next()` on this generator gets `'add'`; calling `next()` a second time gets `'adjust'`; and calling it a third time gets `'affect'`. Every time I run the cell in which `next()` is called on a generator `next()` is called a further time. So I can switch through all items of a generator by running the cell repeatedly.
  Attention: Running the cell in which the generator is created will "reset" the generator. Calling `next()` on the generator will get the first item again.

* The next code retains the sentences in which the current item of `verb_generator` is the a verb and the root of the respective sentence:

  ```python
  [...]
  found_sentences = []
  
  for doc in doc_list:
      for match_id, start, end in initial_search_matcher(doc):
          if doc[start:end].lemma_ == verb_to_prove:
                  
              if doc not in found_sentences:
                  found_sentences.append(doc)
  [...]
  ```

  - The first `for`-loop iterates over all Doc_objects in `doc_list`. For a selected Doc there starts a second `for`-loop that iterates over all tuples in the list that is created by applying `initial_search_matcher()` on the Doc. Roughly, for a selected tuple the program checks if the matched Span is equal to the `verb_generator`-item at hand. If so, it is checked whether the current Doc is already in the list `found_sentences`. (This second if clause shall avoid that a sentence is in the list more than once.) If the Doc is not already in `found_sentences` it will be added to it.

* The next code will give a number to each item in `found_sentences`. And it will print each sentence with its corresponding number:

  ```python
  [...]
  for sentence_number, sentence in enumerate(found_sentences):
      print(f"({sentence_number}) {sentence}")
  ```

* The two preceding code samples are from the cell that starts and ends as follows:

  ```python
  # Next item of 'verb_generator': verb_to_prove
  verb_to_prove = next(verb_generator)
  
  [...]
  
  for sentence_number, sentence in enumerate(found_sentences):
      print(f"({sentence_number}) {sentence}")
  ```

  If you run the cell with this code repeatedly in effect you go through through our list of verbs `root_verb_lemma` item by item. (To be more correct you go through the generator we made from this list.) For each item all sentences are shown in which the verb and root of the sentence is in its lemma form the same as the current item.

* Then we make an empty list with `selected_verbs = []`. In a different cell we code `selected_verbs.append(verb_to_prove)`. If we find a disagreement sentence by running the generator code block we can add the corresponding verb to the list `selected_verbs ` by running `selected_verbs.append(verb_to_prove)`.
  Attention: If you run the cell with `selected_verbs = []` you create it anew as an empty list. All verbs you have added to it will be lost.

(In writing the above code I wanted to achieve two aims: (i) keeping track in the task of selecting verbs; (ii) avoid "Copy and Paste". I hope this is a nice and helpful solution.)

We have the following verbs selected as SUPPORT-verbs: "reveal", "show", "suggest" and "support". Now we are able to make a first verb filter.



### 2.3 A First Verb Filter

The verb filter is supposed to find disagreement sentences with the help of SUPPORT-verbs. For this filter to work we create a new Matcher with a new search pattern. This search pattern will combine the `initial_search_pattern ([{"POS": "VERB", "DEP": "ROOT"}])` with the SUPPORT-verbs.

* `verb_matcher_1 = Matcher(nlp.vocab, validate=True)`: We create a new Matcher (as we have done before) which we call ``verb_matcher_1`.

* We make a search pattern for that Matcher and call it `support_verbs_pattern`:

  ```python
  support_verbs_pattern = [{"POS": "VERB", "DEP": "ROOT", "LEMMA": {"IN": ['reveal', 'show', 'suggest', 'support']}}]
  ```

  The pattern describes a single token (of a Doc) which is a verb an the root of its sentence. In addition the lemmatized form of the token is a member of the list `['reveal', 'show', 'suggest', 'support']` , i. e. the lemma of the token is either "reveal", "show", "suggest" or "support" . This search pattern will be matched by any form of, for example "suggest", like "suggests", "suggested" or "suggesting" etc. (Of course the token has to be a verb and the sentence's root to be matched.) This is the advantage of defining the search pattern by means of the lemma form.
  
* `verb_matcher_1.add("VERB_ID", None, support_verbs_pattern)`: We add the pattern `support_verbs_pattern` to the `verb_matcher_1`.

* We filter `doc_list` and create a list called `verb_filtered_sentences_1` that contains only those items of  `doc_list` that are matched by `verb_matcher_1`:

  ```python
  verb_filtered_sentences_1 = [doc for doc in doc_list if len(verb_matcher_1(doc)) > 0]
  ```

  We create the list `verb_filtered_sentences_1` with what is called in Python a *list comprehension*. It does pretty much the same as the longer code block:

  ```python
  verb_filtered_sentences_1 = []
  
  for doc in doc_list:
      if len(verb_matcher_1(doc)) > 0:
          verb_filtered_sentences_1.append(doc)
  ```

  This happens in the list comprehension (as well as in the alternative code in the code block above): An empty list named `verb_filtered_sentences_1` is created. A `for`-loop iterates over each Doc-object in `doc_list`: It is checked if the Doc meets a certain condition. If it does the Doc is added to the list `verb_filtered_sentences_1`. 
  
  Now to the condition (`if len(verb_matcher_1(doc)) > 0`): When the Matcher `verb_matcher_1()` is applied to the Doc it will create a list. For every Span in the Doc that matches the pattern of the Matcher there will be a tuple in the list. If there is no match the list will be empty. (See 2.1, "Detour: What does a match (=Matcher(Doc)) look like?"). Applying the function `len()` on an appropriate object counts the items of such an object. The function returns the number of items as an **`integer`**. Thus, `len(verb_matcher_1(doc))` yields an integer that says how many tuples are in the list created by running `verb_matcher_1(doc)`. That means the integer tells how many matches there are in the respective Doc. `len(verb_matcher_1(doc)) > 0` says that the number of matches is greater than zero, i. e. that there are one or more matches found in Doc. Hence, the if-clause tells the program to check, whether there is any sequence in Doc that matches the search pattern. If it does this Doc is added to `verb_filtered_sentences_1`. This makes sure that there are only sentences in this list that match the search pattern. All other sentences in `doc_list` are sorted out.
  
  (To see the sentences selected by the verb filter see the corresponding part of "hcq_3_verb_filter.ipynb".)



### 2.4 Evaluation I

Now that we have a first version of the verb filter let's see how good it works. In order to evaluate our verb filter we need a measuring instrument. This is where our Excel spread sheet with labeled sentences comes into play ("sentences_labeled.xlsx"; see "hcq_2_sentences.md" and "hcq_2_sentences.ipynb").

#### 2.4.1 Measuring Device

**For the following steps see:** 
Youtube: ["Intro to NLP with spaCy (3): Detecting programming languages | Episode 3: Evaluation"](https://youtu.be/4V0JDdohxAk)

We are going to load `sentences_labeled.xlsx` as a dataframe. Then we will manipulate it in a few steps in order to use it as a measuring device for evaluation.

* `measuring_df_1 = pd.read_excel("../labeling/sentences_labeled.xlsx")` We load the Excel file into a dataframe and call this dataframe `measuring_df_1`.

* With the following code we create the new column `prediction`. The content of the column is created by a function (**`lambda`** works as a kind of function definition): For every sentence it is asserted that there will be a match by the Matcher `verb_matcher_1`, i. e. the number of matches is greater than zero. This assertion can either be true or false which is expressed by the **`boolean`**s `True` or `False`, respectively. Code:

  ```python
  measuring_df_1 = measuring_df_1.assign(prediction=lambda df: [len(verb_matcher_1(doc)) > 0 for doc in nlp.pipe(df["sentence"])])
  ```

* With the help of the `numpy` library (usually abbreviated as`np`) we convert the `True ` into `1` and `False` into `0`. Code:

  ```python
  measuring_df_1 = measuring_df_1.assign(prediction=lambda df: df["prediction"].astype(np.int8))
  ```

* `measuring_df_1 = measuring_df_1[["sentence", "label", "prediction"]]`: We rearrange the position of columns for dataframe `measuring_df_1`.

The dataframe should now look like this:
![measuring_df_1](/measuring_df_1.png)
*measuring_df_1.iloc[71:76]*

The dataframe `measuring_df_1` now has three columns: one in which all sentences are stored (`sentence`); the label that indicates if we want the sentence to be found by the program (`label`); and the column that says which sentences the program actually find (`prediction`). We are going to use this dataframe and some metrics to assess how good the program (verb filter) works.

#### 2.4.2 Metrics

From `sklearn.metrics`  we have imported to functions to create metrics, that help us to assess how good the verb filter works: `confusion_matrix()` and `classification_report()`. Frankly, I do not know fully what insights these metrics have to offer. First and foremost I want to use these metrics as a starting point to see whether making changes at the verb filter will make things better or worse. Nonetheless I want to give some remarks to the metrics. 

**Confusion Matrix**

We create a so called *Confusion matrix* by running the following code:

```python
confusion_matrix(measuring_df_1["label"], measuring_df_1["prediction"])
```

It yields the following:

```python
array([[200,   2],
       [  6,   8]], dtype=int64)
```

This display shows some sort of table which says something like this:

| **label** | **prediction** |       |
| --------- | -------------- | ----- |
|           | **0**          | **1** |
| **0**     | 200            | 2     |
| **1**     | 6              | 8     |

From this table you can see four label/prediction-combinations: 

* 0/0 (sentences that should not be found and were not found by the program)
* 0/1 (sentences that should not be found but were found ("predicted") by the program - *false positive*)
* 1/0 (sentences that should be found but were not found by the program - *false negative*)
* 1/1 (sentences that should be found and were indeed found by the program)

For each of these label/prediction-combinations the table gives a number. It says how often such a combination occurs. Adding the numbers together gives the total number of sentences in our collection of abstracts: $200 + 2 + 6 + 8 = 216$. We have a total number of 216 Sentences. Each sentence is assigned to exactly one label/prediction-combination.

There are 200 sentences were I have given the value "0" and where the program has given the value "0". These are sentences that I don't the verb filter to find and the verb filter does not find them. Further there are eight (8) sentences which I want the verb filter to find and which it does actually find (1/1). Thus there are 208 (200 + 8) sentences where label and prediction are in accordance. The verb filter deals with 208 sentences correctly.

For the remaining eight (8) sentences label and prediction differ. Two (2) sentences are false positives (0/1). That means that the program finds them or predicts that they should be found whereas I say they should not. (That is like a test indicating that someone has SARS-CoV-2 whereas she is actually not infected.) On the other hand the verb filter is "blind" for six (6) further sentences (1/0). These are the false negatives. Those eight sentences are the mistakes the verb filter is making. And you can see that false negatives are more often than false positives.

**Classification Report**

The next type of metric is the so called *Classification report*. You can yield it by running:

```python
print(classification_report(measuring_df_1["label"], measuring_df_1["prediction"]))
```

This is the result:

```
              precision    recall  f1-score   support

           0       0.97      0.99      0.98       202
           1       0.80      0.57      0.67        14

    accuracy                           0.96       216
   macro avg       0.89      0.78      0.82       216
weighted avg       0.96      0.96      0.96       216
```

Here you see some categories and values. The number in column `support` tells how many instances are taken into account. In our case "216" means that 216 sentences are considered. Or respectively, 202 sentences are labeled by me with "0" and 14 sentences are labeled with "1". 

`precision` says how likely it is that a prediction is true. (Take for example the SARS-CoV-2-test again: `precision` tells how often a person is infected, if the test is positive.) In our case `precision` tells us that for 100 sentences that are predicted as being assigned with a "0" , 97 sentences are act in fact assigned with a "0". And out of 100 sentences that predicted to be labeled as "1", 80 sentences are indeed labeled as "1".

The numbers in column `recall` show how many sentences of the respective type are found by the verb filter. Almost all sentences labeled with "0" are found (99% of the sentences). (Or maybe it would be more correct to say that they are not found since a "0" marks those sentences that the verb filter should not find or recognize as a relevant sentence). But only a bit more than half of the sentences labeled with a "1" are found (57%). That for sentences labeled as "1" the `precision` has a value of 0.80 and the `recall` has a value of 0.57 reflects the fact that there are only two false positives but six false negatives.

For comparing different versions of the verb filter I will consider  the `f1-score` and `accuracy`, too. Though I have to admit, that I am not able to explain what the values in these categories do express.

#### 2.4.3 Mistakes

As a third ingredient of the evaluation I want to look at those sentences that are predicted incorrectly. This shall serve as a foundation for improving the verb filter.

**False Positives**

First to the false positives: 

* We are going to cut `measuring_df_1` into a much smaller dataframe. We name it `false_positives`:

  ```python
  false_positives = measuring_df_1.loc[measuring_df_1["prediction"] == 1].loc[measuring_df_1["label"] == 0, ["sentence"]]
  ```

  These line of code makes use of the fact that every step results in a dataframe so that commands can be concatenated. The first dataframe is `measuring_df_1`. We create a second dataframe with the first `.loc`-statement. `.loc[measuring_df_1["prediction"] == 1]` is selecting those rows where there is a "1" in column `prediction` of dataframe `measuring_df_1`. From these selection of rows we are further selecting a subset of rows. With `.loc[measuring_df_1["label"] == 0, ["sentence"]]` we select those rows that have value "0" in column `label`. And this time only column `sentence` is chosen. These selected rows and column of `measuring_df_1` form a dataframe again. This dataframe `false_positives` encompasses only those sentences of `measuring_df_1` that are predicted as being labeled as "1" but are in fact labeled as "0".

* We print the numbered sentences in `false_positives`. Code:

  ```python
  for sentence_number, sentence in enumerate(false_positives["sentence"].to_list()):
      print(f"({sentence_number}) {sentence}")
  ```

  `false_positives["sentence"]` creates a series that has the content of column `sentence` of dataframe `false_positives`. Since it is a series the method `.to_list()` is applicable. This creates a list of which each item (sentence) is given a number by applying the function `enumerate()` on it. By means of a `for`-loop we advise the computer to print every sentence of the list together with its respective number. The result looks like this:

  ```
  (0) This work was supported by the Emergent Projects of National Science and Technology (2020YFC0844500), National Natural Science Foundation of China (81970020, 81770025), National Key Research and Development Program of China (2016YFC0901104), Shanghai Municipal Key Clinical Specialty (shslczdzk02202, shslczdzk01103), National Innovative Research Team of High-level Local Universities in Shanghai, Shanghai Key Discipline for Respiratory Diseases (2017ZZ02014), National Major Scientific and Technological Special Project for Significant New Drugs Development (2017ZX09304007), Key Projects in the National Science and Technology Pillar Program during the Thirteenth Five-year Plan Period (2018ZX09206005-004, 2017ZX10202202-005-004, 2017ZX10203201-008).
  (1) Current international society recommendations suggest that patients with rheumatic diseases on immunosuppressive therapy should not stop glucocorticoids during COVID-19 infection, although minimum possible doses may be used.
  ```


These sentences are going to be sorted out by the noun filter (yet to come).

**False Negatives**

For the false negatives we proceed in a similar fashion as for the false positives. (Please compare with the notebook "hcq_3_verb_filter.ipynb".) Running the code we get the following result:

```
(0) We were unable to confirm a benefit of hydroxychloroquine or chloroquine, when used alone or with a macrolide, on in-hospital outcomes for COVID-19.
(1) The administration of HCQ did not result in a significantly higher negative conversion probability than SOC alone in patients mainly hospitalized with persistent mild to moderate COVID-19.
(2) Adverse events were higher in HCQ recipients than in HCQ non-recipients.
(3) Although mortality rate was not significantly different between cases and controls, frequency of adverse effects was substantially higher in HCQ regimen group.
(4) Use of these drugs is premature and potentially harmful
(5) Among patients with COVID-19, the use of HCQ could significantly shorten TTCR and promote the absorption of pneumonia.
```

Sentence (0) is a sentence I definitely want to find with the verb filter. I will adapt the search pattern to reach this aim.

The issue with sentences (1) till (5) is that I don't see a structural property by which a program could tell whether they are relevant or not. The make a statement that contributes to answering the question whether HCQ is apt as a treatment for COVID-10 or not. But they don't connect that statement with mentioning evidence and/or a judgment whether the evidence supports the statement or not. Perhaps one could argue that it is not actually a mistake that the verb filter does not detect sentences (1)-(5). I have labeled these sentences with a "1" because within the context of their abstracts the seemed to be the conclusion (drawn upon evidence). But on the other hand comparing these sentences with sentences from other abstracts they seem to be more like statements of evidence or summaries thereof. In addition some of the sentences appear to me very specific - may be too specific to just say if HCQ is apt as a treatment (this is especially true for sentences (1) and (5).) For now I would not mind if the verb filter does not find sentences (1) till (5).



## 3 Verb Filter: Refinements

We have made a first version of a verb filter. Furthermore we have evaluated how good it works and what mistakes it is making. Now we are going to make one improvement. As mentioned the verb filter shall be able to find sentence (0) above. We want to reach this by adding another search pattern to the Matcher.  In essence this will be a repetition of the steps done in subsection "2.3 A First Verb Filter".

The sentence of the false negative results that we want our verb filter to find is:

```
(0) We were unable to confirm a benefit of hydroxychloroquine or chloroquine, when used alone or with a macrolide, on in-hospital outcomes for COVID-19.
```

This sentence is of the sort of sentences we want to find. Though it is not stating that a statement which has something to do with HCQ as a possible treatment for COVID-19 is supported by some evidence, it does something similar.  There is a statement that contributes to the question whether HCQ is apt as a possible treatment for COVID-19: HCQ or CQ do show benefits when treating patients. Let's refer to that statement as $s$. Evidence of some kind is not mentioned, but I assume that a group of scientists ("we") has engaged in a process that results in some sort of evidence. Thus I would paraphrase the sentence above in such a fashion: The findings (produced through research by "we") do not support ("confirm") $s$. "confirm" is a verb and it signifies SUPPORT but it is not found by the verb filter. To see why let us look at some linguistic features. 

* We convert the sentence into a Doc-object (`doc3`):

  ```python
  doc3 = nlp("We were unable to confirm a benefit of hydroxychloroquine or chloroquine, when used alone or with a macrolide, on in-hospital outcomes for COVID-19.")
  ```

* Then we show for each token in the `doc3` the text (string), Part of Speech (which kind of word), Dependency (place in sentence structure) and the lemma:

  ```python
  for token in doc3:
      print(token.text, token.pos_, token.dep_, token.lemma_)
  ```

  We get:

  ```
  We PRON nsubj -PRON-
  were AUX ROOT be
  unable ADJ acomp unable
  to PART aux to
  confirm VERB xcomp confirm
  a DET det a
  benefit NOUN dobj benefit
  of ADP prep of
  hydroxychloroquine NOUN pobj hydroxychloroquine
  or CCONJ cc or
  chloroquine NOUN conj chloroquine
  , PUNCT punct ,
  when ADV advmod when
  used VERB advcl use
  alone ADV advmod alone
  or CCONJ cc or
  with ADP conj with
  a DET det a
  macrolide NOUN pobj macrolide
  , PUNCT punct ,
  on ADP prep on
  in ADP nmod in
  - PUNCT punct -
  hospital NOUN pobj hospital
  outcomes NOUN pobj outcome
  for ADP prep for
  COVID-19 PROPN pobj COVID-19
  . PUNCT punct .
  ```

  "confirm" is indeed a verb but it is not the root of the sentences (or at least the `spacy`-tools do not recognize it as such). `spacy` tells us that "confirm" here is an "open clausal complement" ("xcomp"). We use this information to create an additional search pattern:

  ```python
  confirm_pattern = [{"POS": "VERB", "DEP": "xcomp", "LEMMA": "confirm"}]
  ```

* `verb_matcher_2 = Matcher(nlp.vocab, validate=True)`: We create a new Matcher called `verb_matcher_2`.

  We have to search patterns now the `confirm_pattern` and the `support_verbs_pattern` we have used before in `verb_matcher_1` (see 2.3):

  ```python
  support_verbs_pattern = [{"POS": "VERB", "DEP": "ROOT", "LEMMA": {"IN": ['reveal', 'show', 'suggest', 'support']}}]
  ```

* `verb_matcher_2.add("VERB_ID", None, support_verbs_pattern, confirm_pattern)`. We add both `support_verbs_pattern` and `confirm_pattern` as patterns to the Matcher `verb_matcher_2`.

* `verb_filtered_sentences_2 = [doc for doc in doc_list if len(verb_matcher_2(doc)) > 0]`. By means of `verb_matcher_2` we filter the sentences of `doc_list` again and put the remaining Doc-objects in the list `verb_filtered_sentences_2`.

* We print each Doc in `verb_filtered_sentences_2` with running numbers:

  ```python
  for sentence_number, sentence in enumerate(verb_filtered_sentences_2):
      print(f"({sentence_number}) {sentence}")
  ```

  This time the sentence

  ```
  (0) We were unable to confirm a benefit of hydroxychloroquine or chloroquine, when used alone or with a macrolide, on in-hospital outcomes for COVID-19.
  ```

  is among the results. (See the notebook "hcq_3_verb_filter.ipynb" for the filtered sentences.)



## 4 Evaluation II

In this section I basically repeat the steps from "2.4.1 Measuring Device" and "2.4.2 Metrics". The difference is that I use `verb_matcher_2` instead of the first version of our verb Matcher. And I call the resulting dataframe `measuring_df_2`. The fact that I use the new verb Matcher is probably most crucial in the following line of code:

```python
measuring_df_2 = measuring_df_2.assign(prediction=lambda df: [len(verb_matcher_2(doc)) > 0 for doc in nlp.pipe(df["sentence"])])
```

**Confusion Matrix**

The confusion matrix for the executing the code with `verb_matcher_2` is the following:

| **label** | **prediction** |       |
| --------- | -------------- | ----- |
|           | **0**          | **1** |
| **0**     | 200            | 2     |
| **1**     | 5              | 9     |

In order to better compare the results for the two verb filters I will give the results in a table:

| **matcher**      | **label/prediction-combination** |         |         |         |
| ---------------- | -------------------------------- | ------- | ------- | ------- |
|                  | **0/0**                          | **0/1** | **1/0** | **1/1** |
| `verb_matcher_1` | 200                              | 2       | 6       | 8       |
| `verb_matcher_2` | 200                              | 2       | 5       | 9       |

In comparison with `verb_matcher_1` finds `verb_matcher_2` one relevant sentence more (the value in column "**1/1**" increases from 8 to 9). And the new verb Matcher does one false negative less (the value in column "**1/0**" decreases from 6 to 5). This is in accordance with the fact that we adapted the search patterns in a way that an additional relevant sentence will be found by the verb filter.

**Classification Report**

The values displayed by the classification report have improved, too:

```
              precision    recall  f1-score   support

           0       0.98      0.99      0.98       202
           1       0.82      0.64      0.72        14

    accuracy                           0.97       216
   macro avg       0.90      0.82      0.85       216
weighted avg       0.97      0.97      0.97       216
```

Again, I want to make a table so one might be better able to compare the verb Matchers and see the improvements (I will take only some criteria):

| matcher          | precision 0 | recall 0 | precision 1 | recall 1 | accuracy |
| ---------------- | ----------- | -------- | ----------- | -------- | -------- |
| `verb_matcher_1` | 0.97        | 0.99     | 0.80        | 0.57     | 0.96     |
| `verb_matcher_2` | 0.98        | 0.99     | 0.82        | 0.64     | 0.97     |

In almost all categories considered `verb_matcher_2` yields better results than `verb_matcher_1`. 

In the first two notebooks we took abstracts of scientific articles took the sentences thereof and stored them in tabular form. Besides this, we created a means for evaluating the verb filter. Not all sentences we collected are of interest for the current project so we sought a facility to find those sentences only that are relevant. This was the enterprise of the third notebook with which we dealt here. I started by sketching the idea regarding which sentences are relevant for the project and which features of the sentence structure could be exploited to find the sentences of interest. A main feature are certain verbs that form the root of their respective sentence. Based on this idea we developed a verb filter, assessed its performance and improved it. But the verb filter is only one component of a overarching filter. Another component is a filter that searches for certain noun, i. e. nouns indicating evidence. The development and evaluation of such a filter will be the task of the next notebook.



