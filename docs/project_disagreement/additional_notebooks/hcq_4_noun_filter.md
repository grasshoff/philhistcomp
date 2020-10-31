#### PHILOSOPHY AND HISTORY OF SCIENCE WITH COMPUTATIONAL MEANS

##### PROF. DR. GERD GRAßHOFF



# 4 Noun Filter

As said in "hcq_3_verb_filter.md" the verb filter is only one part of an overarching filter. To get the relevant sentences from our sentence collection in a more reliable way another filter is needed: the noun filter. The development of the noun filter is the main task of the present notebook "hcq_4_noun_filter.ipynb". The first three sections are a concise repetition of the preceding notebook "hcq_3_verb_filter.ipynb". The result will be a verb filtered list of sentences which is the material the noun filter has to work with. In the development of the noun filter I will proceed as in the development of the verb filter (shown in the preceding notebook): First I will develop an initial search pattern for nouns. This will be the grounding work for the search pattern to use in the first version of the noun filter. The noun filter's performance shall be assessed and compared to the performance of the verb filter alone. Please keep in mind that the noun filter comes into play after the verb filter has been applied. The noun filter will never operate isolated but only combined with the verb filter. Second I will try to improve the noun filter or, to be more precise, the search pattern of the noun filter. Finally, the (hopefully) improved noun filter shall be evaluated a second time.



## 0 Import Libraries

Import the required libraries and the language model.



## 1 Load Dataframe

Load the data stored in `HCQ_sentences.json` in the **`dataframe`** `sentences_df`.



## 2 Verb Filtered Sentences

We filter the sentences in `sentences_df` by means of the verb filter developed in "hcq_3_verb_filter.ipynb" (see also "hcq_3_verb_filter.md"). The result is a list that contains the remaining sentences. The list is named `verb_filtered_sentences_2`.

The steps in these sections are executed and described in the preceding notebooks and corresponding markdown-files, respectively. Besides this the focus of the current markdown-file is on the following code. That is why I treat the earlier steps only very briefly. 



## 3 Noun Filter

As was said in  "hcq_3_verb_filter.md" (2, detour) the sentences I want to find in this project - the relevant sentences - express that a certain statement $s$ is supported by some body of evidence. With the help of the verb filter we can find sentences, in which verbs occur that might express the (meta language) SUPPORT-relation. But though the right verbs appear in the right position of the structure of a sentence, that sentence might not express that $s$ is supported by evidence. That is, the verb filter might have found a sentence and thereby marked it as relevant whereas it is not a relevant sentence. Such misleading findings make the results less reliable. In order to make the search results more reliable there should occur certain nouns in the filtered sentences, in addition to the SUPPORT-verbs. These nouns refer to some type of evidence or to a process producing such evidence. I call such nouns EVIDENCE-nouns. In some cases not the EVIDENCE-nouns are mentioned but "placeholders" that stand for scientists or scientific institutions that yield such evidence. The hope is: if both SUPPORT-verbs and EVIDENCE-nouns occur together in a sentence it is more likely that the sentence expresses the SUPPORT-relation between evidence and $s$. That is why we are going to make a noun filter.

(This present markdown-file is **not** a substitute for the corresponding notebook - it is a complement. Therefore it is strongly recommended that you read it alongside with the corresponding notebook. This holds for every markdown-notebook-combination. I hope it is clear which description refers to which line(s) of code and vice versa, although the code is not displayed here.)

(If you want to know more about the technical details, please have a look at "hcq_3_verb_filter.md".)

In this section we will make an initial search pattern which lays the ground for developing a first version of the noun filter. At the end we will test how good the verb filter together the first version of the noun filter perform in finding disagreement sentences.

### 3.1 An Initial Search Pattern

We will make and test a first search pattern by which we can find EVIDENCE-nouns. This search pattern is just an approximation. The found nouns will have to be selected manually (next subsection). We start by looking at our example sentence turned into the Doc-object `doc`. An analysis with the help of `spacy` shows that there are more nouns in `doc` than we want to find. Since we are looking for sentences which express a SUPPORT-relation between evidence and $s$  and the SUPPORT-relation is expressed by the main verb of the sentence (as is assumed) only nouns are promising that are at a certain position of the sentence structure. It is the nouns that are the *nominal subject* (`nsubj`) that are most promising. We make the search pattern `initial_search_pattern_noun` which will find nouns that are the nominal subject of a sentence. (At last we test that search pattern on `doc`.)

### 3.2 List of EVIDENCE-nouns

Here we will create a hand picked list of EVIDENCE-nouns. First we make a Matcher-object - the `initial_search_matcher_noun` - which works with the search pattern `initial_search_pattern_noun` that we have created in the previous subsection. By means of this Matcher we search for nouns that are also nominal subjects. We carry out this search in the list of verb filtered sentences `verb_filtered_sentences_2`. As a result we get a list of nouns in their lemma form.

In the next step we are going to pick out those nouns that refer to evidence or the process of gaining evidence. For this purpose:  If a certain noun is the nominal subject of a sentence, we check whether this sentence is a disagreement sentence. We also rely on a common understanding of what the respective nouns mean. As a result we get five nouns qualified as EVIDENCE-nouns: "analysis", "evidence", "finding", "result" and "survey". These nouns will serve as the foundation for creating the first version of our noun filter.

(Be careful! Do **not** run the cell with code `selected_nouns = []` more than once unless you intent to reset the list as an empty one.)

### 3.3 A First Noun Filter

The goal is to get a list of sentences that are filtered both by the verb filter and by the noun filter. So there are two processes of selection. First we create a new Matcher-object - `noun_matcher_1` - , some search patterns and add these patterns to `noun_matcher_1` . Second we filter the verb filtered sentences in `verb_filtered_sentences_2` by means  of `noun_matcher_1`  and add the remaining sentences to a list. The resulting list of sentences is called `noun_filtered_sentences_1`. Our collection of sentences originally encompassed 216 sentences. Applying the verb filter and then the noun filter on this collection leaves us with seven sentences.

### 3.4 Evaluation I

(For the following steps see: 
Youtube: ["Intro to NLP with spaCy (3): Detecting programming languages | Episode 3: Evaluation"](https://youtu.be/4V0JDdohxAk)) 

In this subsection we want to assess how good our `noun_matcher_1`  works. This will set the stage for improving it. We will assess `noun_matcher_1` using two metrics - the *confusion matrix* and the *classification report*. In addition we want to see which mistakes the Matcher is making. But before that we have to create a measuring device.

#### 3.4.1 Measuring Device

The measuring device shows where the computer and I agree or differ in telling the disagreement sentences from the other ones. I call my judgment the *label* of the sentences whereas the computer's judgment is called *prediction*. We aim to display this in tabular form. "1" shall mean that a sentence is recognized as a disagreement sentence (or relevant); "0" shall mean that a sentence is not a disagreement sentence.

I have prepared an Excel spread sheet where I labeled the sentences (see "hcq_2_sentences.ipynb" and "hcq_2_sentences.md"). In a first step we load the spread sheet as a dataframe and give it the name `measuring_df_3`. 

The list `noun_filtered_sentences_1` contains sentences that are filtered by the verb filter and the noun filter. Regarding the *datatype* each of the sentences is a Doc-object. We convert each Doc-type sentence into a string and store the string-type sentences in the list `noun_filtered_strings_1`. For a string it can be checked whether it is contained in a list or not. This is feature is important for the next steps.

Third, we define the function `compare_sentences()`. The function takes a sentences (from a dataframe) as an argument and returns a **`boolean`**  as the result. A boolean (abbreviated as **`bool`**) can have two values: `True` and `False`. What is called in Python terminology a bool is called in more philosphical terms a truth value. How does `compare_sentences()` generate a boolean? It checks whether a sentence from `measuring_df_3["sentence"]` (column  `sentence` of dataframe`measuring_df_3`) is in the list `noun_filtered_strings_1`. It checks whether the statement that a string from `measuring_df_3["sentence"]` is contained in `noun_filtered_strings_1` is true or false. If a string from the dataframe is in the list `noun_filtered_strings_1` the statement is true and the bool `True` is returned. Otherwise the `False` is returned. This is the code for the function definition:

```python
def compare_sentences(sentence):
    return sentence in noun_filtered_strings_1
```

Then we apply `compare_sentences()` to each row of `measuring_df_3`. Thus we create the new column `prediction` which contains booleans only. `noun_filtered_strings_1` contains strings of sentences. The sentences are only those ones that were found by both filters - the verb filter and the noun filter. Hence only those sentences in `measuring_df_3["sentence"]` can have the value `True` that would be found by both filters. That means, from all sentences of our original collection the ones are marked that would be found by the combined fiters. If the computer "thinks" a sentence is a disagreement sentence then a `True ` is assinged to it, otherwise the sentence gets a `False`.

Fourth, we convert the booleans in column `prediction` . Each `True` is turned into `1` and each `False` turned into `0`. If the computer recognizes a sentence as a disagreement sentence then it gets a `1` , otherwise the sentences gets a `0`. I have assigned `1`s and `0` to sentences in the same fashion. (This was the task of labeling.)

Finally, we have a means to compare the judgments done by me (a human being) and done by the computer. We can check where both assign a `1` (or a `0`, respectively) to a sentence. And we can see if assignments differ. This measuring device is used by the following metrics.

#### 3.4.2 Metrics

**Confusion Matrix**

The following confusion matrix is created:

| **label** | **prediction** |       |
| --------- | -------------- | ----- |
|           | **0**          | **1** |
| **0**     | 202            | 0     |
| **1**     | 7              | 7     |

209 sentences are predicted correctly ($202 + 7$). Seven disagreement sentences (at least in my view) were not predicted by the computer. The comparison with earlier filters shows the following:

| **matcher**                         | **label/prediction-combination** |         |         |         |
| ----------------------------------- | -------------------------------- | ------- | ------- | ------- |
|                                     | **0/0**                          | **0/1** | **1/0** | **1/1** |
| `verb_matcher_1`                    | 200                              | 2       | 6       | 8       |
| `verb_matcher_2`                    | 200                              | 2       | 5       | 9       |
| `verb_matcher_2` & `noun_matcher_1` | 202                              | 0       | 7       | 7       |

The combination of verb filter and noun filter is not in all aspects better than the refinded verb filter. `verb_matcher_2` finds more disagreement sentences (1/1). But verb filter and noun filter together declare two more irrelevant sentences correctly as irrelevant (0/0). And what is more important: The two filters together do not catch false positives. That means, if the computer "says" that a sentence is a disagreement sentence, you can be sure that it is one (at least in my opinion).

**Classification Report**

The classification report for the two filters looks like this:

```
              precision    recall  f1-score   support

           0       0.97      1.00      0.98       202
           1       1.00      0.50      0.67        14

    accuracy                           0.97       216
   macro avg       0.98      0.75      0.82       216
weighted avg       0.97      0.97      0.96       216
```

Let's give an overview to compare it with the earlier filters:

| matcher                             | precision 0 | recall 0 | precision 1 | recall 1 | accuracy |
| ----------------------------------- | ----------- | -------- | ----------- | -------- | -------- |
| `verb_matcher_1`                    | 0.97        | 0.99     | 0.80        | 0.57     | 0.96     |
| `verb_matcher_2`                    | 0.98        | 0.99     | 0.82        | 0.64     | 0.97     |
| `verb_matcher_2` & `noun_matcher_1` | 0.97        | 1.00     | 1.00        | 0.50     | 0.97     |

The verb filter and nounfilter sort out all non disagreement sentences (recall 0 = 1.00). But not each sentence that is marked as irrelevant is indeed not a disagreement sentence. If the filters combinded mark a sentence as a disagreement sentence you can rely on that. This says the same as the fact that there are not false positves. In this respect the comination of verb filter and noun filter performs far better than the verb filter alone. But the filters together find only half of all disagreement sentences. This is the worst result of all filters compared.

#### 3.4.3 Mistakes

**False Positives**

As has been said the combination of `verb_matcher_2` and `noun_matcher_1` yield no false positives. If a sentences is predicted as being a disagreement sentence you can be sure that it is one (at least with regard to my labeling).

**False Negatives**

Disagreement sentences that are not found by the combination of the two filters are:

```
(0) We were unable to confirm a benefit of hydroxychloroquine or chloroquine, when used alone or with a macrolide, on in-hospital outcomes for COVID-19.
(1) The administration of HCQ did not result in a significantly higher negative conversion probability than SOC alone in patients mainly hospitalized with persistent mild to moderate COVID-19.
(2) Adverse events were higher in HCQ recipients than in HCQ non-recipients.
(3) Although mortality rate was not significantly different between cases and controls, frequency of adverse effects was substantially higher in HCQ regimen group.
(4) Use of these drugs is premature and potentially harmful
(5) Among patients with COVID-19, the use of HCQ could significantly shorten TTCR and promote the absorption of pneumonia.
(6) Chloroquine phosphate, an old drug for treatment of malaria, is shown to have apparent efficacy and acceptable safety against COVID-19 associated pneumonia in multicenter clinical trials conducted in China.
```

I definitvely want the filters to find sentences (0) and (6). In my opinion it is a mistake that the filters together mark them as irrelevant. But you could argue if it is a mistake that sentences (1) till (5) are not counted as disagreement sentences. One reason could be that they merely state evidence than stating that some statemant about HCQ is (not) supported by some evidence. (I briefly discuss this in "hcq_3_verb_filter.md", 2.4.3.) For now I wouldn't mind if the filters do not find them.



## 4 Noun Filter: Refinements

We now have a first version of the noun filters that shall run together with the verb filter. As our assessment has shown the two filters together do not find all disagreement sentences. Let's try to modify our noun filter in such a way that it fill also find sentences (0) and (6).

Again, sentence (0) reads:

```
(0) We were unable to confirm a benefit of hydroxychloroquine or chloroquine, when used alone or with a macrolide, on in-hospital outcomes for COVID-19.
```

In (0) there is a statement that contributes to the question whether HCQ/CQ is apt to treat COVID-19. Further there is a verb expressing that this statement is supported ("confirm") - but in this case the support is negated. What is missing is a noun that stands for evidence. The nominal subject of the sentence is not an EVIDENCE-noun. But in the sentence as the nominal subject there occurs a "[w]e". One could take "[w]e" as a placeholder for an EVIDENCE-noun. In my view it refers to scientists (or a scientific institution) that do research and thereby produce evidence.

According to the `spacy`-analysis "[w]e" is not a noun but a pronoun. And it is the nominal subject of the sentence. Together with the coressponding lemma we can create this pattern:

```python
we_pattern = [{"POS": "PRON", "DEP": "nsubj", "LEMMA": "-PRON-"}]
```

Sentence (6) would fit our usual patterns if it were in active form, like this reformulation:

```
Multicenter clinical trials conducted in China show that chloroquine phosphate [...] has apparent efficacy and acceptable safety against COVID-19 associated pneumonia.
```

(If you want you can try out a `spacy`-analysis of this reformulation.)

But (6) is formulated in passive form and reads like this:

```
(6) Chloroquine phosphate, an old drug for treatment of malaria, is shown to have apparent efficacy and acceptable safety against COVID-19 associated pneumonia in multicenter clinical trials conducted in China.
```

The noun at the position of the nominal subject is not an EVIDENCE-noun, but it is "phosphate". The noun "trials" refers to a process that results in evidence. It is therefore counted as an EVIDENCE-noun. `spacy` tells us that "trials" is an "object of preposition" (`pobj`). Together with the lemma of "trials" we make the following pattern:

```python
trial_pattern = [{"POS": "NOUN", "DEP": "pobj", "LEMMA": "trial"}]
```

We create the new Matcher `noun_matcher_2` and add all search patterns for nouns to it. Then we filter the sentences in `verb_filtered_sentences_2` again - but this time with `noun_matcher_2`  - and store the retained Doc-objects in the list `noun_filtered_sentences_2`. The sentences (0) and (6) are now among the found sentences (but sentence (6) is now sentence (8)). Of 216 sentences in the original collection 11 sentences remained after applying the verb filter; nine are left after additionally using the noun filter.



## 5 Evaluation II

After modifying the noun filter we want to see whether the performance of the two filters working together has improved. The steps are in essence the ones taken in 3.4 - but this time for `noun_matcher_2`  or `noun_filtered_strings_2`, respectively. Thus I will skip a detailed description.

**Confusion Matrix**

The confusion matrix now shows this:

| **label** | **prediction** |       |
| --------- | -------------- | ----- |
|           | **0**          | **1** |
| **0**     | 202            | 0     |
| **1**     | 5              | 9     |

The two filters predict 211 ($202 + 9$) sentences correctly. Five disagreement sentences (if they are such) are not found. There are no false positives. So the filters produce five ($5 + 0$) mistakes. In comparison the filters show the following results:

| **matcher**                                | **label/prediction-combination** |         |         |         | correct | mistakes |
| ------------------------------------------ | -------------------------------- | ------- | ------- | ------- | ------- | -------- |
|                                            | **0/0**                          | **0/1** | **1/0** | **1/1** |         |          |
| `verb_matcher_1`                           | 200                              | 2       | 6       | 8       | 208     | 8        |
| `verb_matcher_2`                           | 200                              | 2       | 5       | 9       | 209     | 7        |
| `verb_matcher_2` & `noun_matcher_1` (V2N1) | 202                              | 0       | 7       | 7       | 209     | 7        |
| `verb_matcher_2` & `noun_matcher_2` (V2N2) | 202                              | 0       | 5       | 9       | 211     | 5        |

Together `verb_matcher_2` and `noun_matcher_2` (let's call this combination V2N2) discriminate 211 sentences correctly and make five mistakes.

* In all categories V2N2 is better than `verb_matcher_1` which predicts 208 ($200 + 8$) sentences correctly and makes eight ($2 + 6$) mistakes.
* `verb_matcher_2` alone predicts 209 ($200 + 9$) sentences correctly and makes seven ($2 + 5$) mistakes. It is as good as V2N2 in finding disagreement sentences (9) and misses the same number of disagreement sentences (5) as V2N2. But it produces two more false positives than V2N2 and sorts out two irrelevant sentences less. V2N2 performs better than `verb_matcher_2` .
* V2N1 predicts 209 ($202 + 7$) sentences correctly and makes seven ($7 + 0$) mistakes. Both V2N1 and V2N2 yield no false positives and they mark equally many non disagreement sentences as irrellevant (202). But V2N1 finds two disagreement sentences less and (therefore) produces two false negatives more.  V2N2 performs better than V2N1.

Of all tested filters or combinations of filters the combination of `verb_matcher_2` and `noun_matcher_2` (V2N2) yields the best results.

**Classification Report**

The classification report for `noun_matcher_2` looks like this:

```
              precision    recall  f1-score   support

           0       0.98      1.00      0.99       202
           1       1.00      0.64      0.78        14

    accuracy                           0.98       216
   macro avg       0.99      0.82      0.89       216
weighted avg       0.98      0.98      0.97       216
```

Let's compare it with the other filters or combination of filters:
| matcher                                    | precision 0 | recall 0 | precision 1 | recall 1 | accuracy | total |
| ------------------------------------------ | ----------- | -------- | ----------- | -------- | -------- | ----- |
| `verb_matcher_1`                           | 0.97        | 0.99     | 0.80        | 0.57     | 0.96     | 4.29  |
| `verb_matcher_2`                           | 0.98        | 0.99     | 0.82        | 0.64     | 0.97     | 4.40  |
| `verb_matcher_2` & `noun_matcher_1` (V2N1) | 0.97        | 1.00     | 1.00        | 0.50     | 0.97     | 4.44  |
| `verb_matcher_2` & `noun_matcher_2` (V2N2) | 0.98        | 1.00     | 1.00        | 0.64     | 0.98     | 4.60  |

In no category V2N2 is worse that any other filter or combination of filters. V2N2 reaches values in every category that are at least as good as the best value reached by another filter (or combination thereof). With 0.98 V2N2 yields the best accuracy value of all (combination of) filters. 

In comparing the filters and filter combinations it might be helpful to add the values of each category:
$$
total = (precision\ 0) + (recall\ 0) + (precision\ 1) + (recall\ 1) + (accuracy)
$$
Comparing the values of column "total" V2N2 has the highest value (4.60). V2N2 yields the best results of all compared filters and combinations of filters. So I want to stick with V2N2. What is good about V2N2 is that if it classifies a sentence as a disagreement sentence then one can be sure that it is one. (Or at least one can be sure that I would have labeled it as a disagreement sentence.) But the recall of disagreement sentences is not that goog (recall 1 = 0.64). V2N2 finds only round about two thirds of all disagreement sentences. Depending on the purpose the precision might be more important than the recall (or vice versa). The overall goal of this project is to form pairs of sentences in such a fashion that the pair resembles disagreement. For this purpos precision might be a lot more important than recall. If this is correct than I don't have to bother about optimizing the recall.



In this notebook we have created a noun filter. The verb filter and the noun filter are components of a combined filter that shall find disagreement sentences. We have evaluated the combined filter V2N2 and improved it. V2N2 finds disagreement sentences in a reliable way. As I have mentioned before disagreement sentences have certain linguistic features which the V2N2 uses to identify them. (But I don't mean to say that all and only disagreement sentences - as I conceive them within the context of this project - have these linguistic features. It is merely a starting point for inquiry.) Which are those features? Disagreement sentences express a (metalanguage) relation - the support relation. This relation can be negated. The relata are (i) what I have called EVIDENCE-nouns; and (ii) a statement about the circumstance that hydroxychloroquine/chloroquine can treat COVID-19. The noun filter shall find EVIDENCE-nouns. The verb filter shall find SUPPORT-verbs which express the SUPPORT-relation. In such a way i hope to find disagreement sentences. In the end I want to form pairs of disagreement sentences such that they show disagreement. In the next notebook I want to make SUPPORT-verbs and EVIDENCE-nouns more visible; and not to forget expressions of negation. Thus what we are going to do in the next notebook is - in the terminology of `spacy`- *Entity labeling*.