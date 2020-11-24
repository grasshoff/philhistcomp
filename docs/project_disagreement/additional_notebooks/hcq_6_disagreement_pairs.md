#### PHILOSOPHY AND HISTORY OF SCIENCE WITH COMPUTATIONAL MEANS

##### PROF. DR. GERD GRAßHOFF

This markdown file has to be complemented by the corresponding notebook. It is strongly recommended to read the markdown file alongside the notebook.

For the previous steps see:

* [hcq_1_clean_abstracts.md](hcq_1_clean_abstracts.md)
* [hcq_2_sentences.md](hcq_2_sentences.md)
* [hcq_3_verb_filter.md](hcq_3_verb_filter.md)
* [hcq_4_noun_filter.md](hcq_4_noun_filter.md)
* [hcq_5_entity_labels.md](hcq_5_entity_labels.md)



# 6 Disagreement Pairs

In the last notebook/markdown file we took the filtered sentences and labeled entities of interest occurring in these sentences. In addition we separated the sentences into sentences that have a negation and sentences that have not. In this current notebook/markdown we want to enable the computer to find sentence pairs that express disagreement. The steps to reach this aim are described in section 6 and the focus is on this task. A key to distinguish disagreement pairs from sentence pairs that do not express disagreement is the Span similarity of a sentence pair. We will mainly evaluate which similarity threshold gives the best results.

## 0 Import Libraries

Import the required libraries and the language model.



## 1 Load Dataframe

Load the data stored in `HCQ_sentences.json` in the **`dataframe`** `sentences_df`. (This collection of sentences encompasses 216 sentences.)

See: [hcq_2_sentences.md](hcq_2_sentences.md)



## 2 Verb Filtered Sentences

We filter the sentences in `sentences_df` by means of the verb filter developed in "hcq_3_verb_filter.ipynb" (see also [hcq_3_verb_filter.md](hcq_3_verb_filter.md)). The result is a **`list`** that contains the eleven remaining sentences. The list is named `verb_filtered_sentences_2`.

See: [hcq_3_verb_filter.md](hcq_3_verb_filter.md)

​          

## 3 Noun Filtered Sentences

In addition we use the `noun_matcher_2` to filter the sentences in `verb_filtered_sentences_2`. We have developed the noun filter in "hcq_4_noun_filter.ipynb" (see also [hcq_4_noun_filter.md](hcq_4_noun_filter.md) for a description). The remaining nine sentences are stored in the list `noun_filtered_sentences_2`. There were 216 sentences in our original collection (`sentences_df`) of sentences. After successively applying the verb filter and the noun filter we end up with nine sentences in  `noun_filtered_sentences_2`. (The sentences in `noun_filtered_sentences_2` are of datatype **`string`**; they are **not** **`Doc`**-objects! )

See: [hcq_4_noun_filter.md](hcq_4_noun_filter.md)



## 4 Label Entities

After filtering the relevant sentences (*disagreement sentences*) from our original collection of sentences, we now label entities occurring in these sentences. We define and label the following entities:

+ **`SUPP`**: SUPPORT-verb
+ **`EVID`**: Evidence or research process producing evidence; **`SCI`**: Scientist(s) or scientific institution
+ **`NEG`**: Negation.

The entities are labeled by nlp-processing the strings in `noun_filtered_sentences_2`. By this the strings are converted into Doc-objects. We store the Docs in the list `disagreement_sentences_5`.

See: [hcq_5_entity_labels.md](hcq_5_entity_labels.md)



## 5 Separate Sentences with Regard to Negation

We separate the sentences in `disagreement_sentences_5`. Sentences in which there is no negation (*affirmative sentences*) are stored in the list `sents`. Whereas sentences with a negation (*negated sentences*) are stored in the list `negated_sents`. This step is a preparation for forming sentence pairs which in turn lays the ground for getting pairs of sentences that express disagreement (*disagreement pairs*).

See: [hcq_5_entity_labels.md](hcq_5_entity_labels.md)



## 6 Disagreement Pairs

The aim is to make the computer find pairs of sentences that express disagreement. At the end we shall show those sentence pairs that the computer recognized as disagreement pairs. We will show them with labeled entities.

### 6.1 Preparing Evaluation

The main part of this notebook is to assess how good the computer can distinguish disagreement pairs from sentence pairs that don't express disagreement. A key component for distinguishing sentence pairs is the similarity of certain parts of the paired sentences (*Span similarity*). We want to find that Span similarity value at which the computer performs best. For assessing the computers performance, in this subsection we are going to make a measuring device. (We have done this in a similar fashion for testing verb and noun filters. See: [hcq_2_sentences.md](hcq_2_sentences.md), [hcq_3_verb_filter.md](hcq_3_verb_filter.md) and [hcq_4_noun_filter.md](hcq_4_noun_filter.md).)

#### 6.1.1 Make Sentence Pairs for Labeling

The first step in making our measuring device is to create a dataframe in which the sentence pairs are stored. Each sentence pair will by supplied by a label indicating whether it is a disagreement pair or not. Disagreement pairs will get the label "1". Sentence pairs that do not express disagreement will get the label "0".

We'll begin by pairing sentences. We will combine each sentence in `sents` with every sentence in `negated_sents`. There are five sentences in `sents` and four sentences in `negated_sents`. So we get 20 sentences pairs. Each pair consists of one affirmative sentence from `sents` and one negated sentence from `negated_sents`. We store the pairs in a dataframe called `sentence_pairs_df`. A column `label` with default value "0" is added. Then we export the dataframe as an Excel spread sheet (`sentence_pairs_to_label.xlsx`) . I manually label the sentence pairs in the Excel file and save the resulting file as `sentence_pairs_labeled.xlsx`.  I load this second Excel file as the dataframe `df_labeled_pairs`.

#### 6.1.2 Span Similarity

In this subsubsection we'll compute the Span similarity for each sentence pair and add it in a new column to `df_labeled_pairs`. I assume that for any to sentences to express disagreement they have to be about (roughly) the same topic. Further I assume that the topic which is the sentence about is indicated in that part of the sentence which comes behind the SUPPORT-verb. (A part of a Doc-object is called a Span.) We use the `span_matcher` to cut off the "topic Span" from the rest of the sentence. For each sentence pair we take the Spans and compute how similar they are.

Then we add a further column --- `prediction` --- to `df_labeled_pairs`. We set a certain value as threshold for Span similarity (here for example "0.84"). It is claimed that the Span similarity for a sentence pair is at least as high as the threshold. If this is true the value `True` is added in column `prediction` in the row of that sentence pair. If the Span similarity is lower than the threshold the value `False` is added. Afterwards we convert each `True` into `1` and each `False` into `0`. 

Now we can compare the prediction of the computer with the labeling by me. If for a row the values in column `label` and column `prediction` are the same then we both judge the respective sentence pair in the same way --- either as being a disagreement pair or not. Different values indicate differing judgments. We pretend that my labeling tells the truth about a sentence pair. So comparing the values of columns `label` and `prediction` shows whether the computer correctly recognizes disagreement pairs and non disagreement pairs. And it shows where the computer is making a mistake.

The basic idea is: It is assumed that disagreement pairs show a certain degree of Span similarity. That you can distinguish disagreement pairs form other sentence pairs by means of the Span similarity. One sets a Span similarity threshold and if the span similarity for a pair is as high as the threshold or higher, then the computer will predict the pair as being a disagreement pair. If the Span similarity is lower than the threshold, then the computer will predict it as not being a disagreement pair.

The task is to find the right threshold. If it is too low all sentence pairs will be predicted as being disagreement pairs. If it is too high no pair will be predicted as being a disagreement pair. The aim is to find a threshold such that the computer predicts as much sentence pairs in accordance with my labeling as possible. This is the task for the evaluation subsection. We will play through several threshold values. That means we will create confusion matrices and classification reports for several threshold values. 

The lowest Span similarity for a sentence pair of our collection is `0.693679928779602` and the highest Span similarity is `0.8892055749893188`. Thus, we will search for the threshold value in an interval ranging from `0.69` to `0.9`. Additionally, we will compare confusion matrices and classification reports for the average Span similarity of all sentence pairs (`0.8331354290246964`); for the average Span similarity of the sentence pairs I labeled as "1" (`0.8332407027482986`); and for the average Span similarity of the sentence pairs I labeled as "0" (`0.8329775184392929`). 

### 6.2 Evaluation

#### 6.2.1 Confusion Matrix

The results show that for a Span similarity threshold of 0.83 the computer predicts the most sentence pairs correctly. 13 sentence pairs out of 20 are correctly predicted.

#### 6.2.2 Classification Report

Likewise the classification reports show that a Span similarity threshold of 0.83 yields the best results. If the computer marks a sentence pair as being a disagreement pair, this will be correct in 69 %  of the cases (`prediction 1`). 

### 6.3 Display Disagreement Pairs

Finally, we will display disagreement pairs with labeled entities. In order to do this we define a function (`disagreement_pairs()`) that separates disagreement pairs from non disagreement pairs. A pair of sentences will be taken to be a disagreement pair if the Span similarity for that pair is at least 0.83.



We have written a program that detects sentence pairs which express disagreement. The disagreeing sentences are displayed with labeled entities.

