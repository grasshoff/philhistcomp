# Expressing Disagreement in Abstracts

Tobias Kittner



### A Reflection on Scientific Articles about Hydroxychloroquine (HCQ)/Cholorquine (CQ) as a Treatment for COVID-19



## Background

Disagreement is a subject of philosophical investigation. As a fist approximation you can characterize disagreement as follows: Disagreement is directed towards some assertion $s$. You can have different doxastic attitudes towards $s$. You can judge that $s$ is true; you can judge that $s$ is false; or you can suspend judgment. Two individuals disagree if they have different doxastic attitudes with regard to $s$. (For a introduction see: [Frances/Matheson 2019](https://plato.stanford.edu/archives/win2019/entries/disagreement/)).

My project is a case study about disagreement in science. The matter of debate had to be a controversial topic in order to find disagreement expressed in scientific literature. Though the epistemic position of scientists disagreeing with each other is unknown yet it is plausible to assume that each of them is in a good epistemic position with regard to $s$. That means they are more likely to judge the truth value of $s$ correctly than to be mistaken. Since scientists are competent evaluators of $s$'s truth value it is interesting how they can judge the truth of $s$ differently.

The matter of debate for this project is whether Hydroxychloroquine (HCQ)/Chloroquine (CQ) is effective and safe enough to treat COVID-19. So in our case $s$ will be something like "HCQ is apt as a treatment for COVID-19". I suppose that one form in which disagreement in science occurs is that it is embedded in discourse. To trace disagreement I shall look for certain sentences that mark a position within this discourse. But I will confine myself to sentences that appear in abstracts of scientific articles.



## Research Objects

**Research data**
With the help of the ["Global research on coronavirus disease (COVID-19)"](https://www.who.int/emergencies/diseases/novel-coronavirus-2019/global-research-on-novel-coronavirus-2019-ncov/)-database of the *World Health Organization (WHO)* I searched for scientific publications that are about HCQ/CQ as a possible medication for COVID-19. I found 19 articles. I used the [DIMENSIONS](https://app.dimensions.ai/discover/publication) database to generate an Excel spread sheet with metainformation concerning these 19 publications (`"hcq.xlsx"`). By means of `"hcq.xlsx"` I could access abstracts of the 19 publications in machine readable form. (`"hcq.xlsx"` is in the directory `"data"`: `"data/hcq.xlsx"`.)

**Research objects**
The research objects are sentences from abstracts that can be grouped in pairs such that a sentence pair expresses disagreement. I will call such sentences *disagreement sentences*. Pairs of disagreement sentences that express disagreement I will call *disagreement pairs*.

**Research question**
What features do disagreement sentences have? How can they be arranged into disagreement pairs? 
(I have a hypothesis about what features disagreement sentences have and how they can be arranged to disagreement pairs. My aim is to write a program (notebook) that can reliably find disagreement pairs. By this I hope to gain support for the hypothesis. I will come back to that later.)



## Confinements

Disagreement may be expressed in various ways. As I said I will only consider certain sentences occurring in abstracts of scientific articles. Thus expressions of disagreement are in writing. In addition I will only consider disagreement that is expressed by *pairs* of sentences. Let's look at some examples of what disagreement pairs might look like (the examples are not exhaustive):

The affirmative sentence:

> (PRO): "The study shows that HCQ is good as a treatment for COVID-19."

To form a disagreement pair the affirmative sentence is to be combined with one of the following negated sentences:

> (CON 1): "The study shows that HCQ is bad as a treatment for COVID-19."
>
> (CON 2): "Because the study is poorly done you can draw no conclusions from it about HCQ as a treatment for COVID-19."
>
> (CON 3): "The study does not show that HCQ is good as a treatment for COVID-19."

Each of the CON-sentences forms a disagreement pair together with the PRO-sentence. The program is designed to only find disagreement pairs of the form (PRO, CON 3). Furthermore the program will only work for sentences in English.



## The Basic Idea

I assume disagreement sentences to have a certain structure. They express a relation between some kind of evidence and a statement $s$. I call that relation *SUPPORT*. (I suppose SUPPORT is logically a relation of the metalanguage. I think of it being similar to the relation of deducibility that may hold between a set of formulas and some single formula.) In disagreement sentences there are certain verbs that mark SUPPORT. I will call such verbs *SUPPORT-verbs*. Then there are linguistic expressions I'll call *EVIDENCE-nouns*.  They refer to evidence of some type, processes resulting in evidence or scientists/scientific institutions that produce such evidence. Disagreement sentences can be affirmative or negated. To illustrate this idea I give the following table. (The sentences are real examples from the abstracts.)
| Sentence type | Noun (EVID/SCI) | Negation (NEG) | Verb (SUPP) | Statement (Span)                                             |
| ------------- | --------------- | -------------- | ----------- | ------------------------------------------------------------ |
| PRO           | `The findings`  |                | `support`   | `the hypothesis that these drugs have efficacy in the treatment of COVID-19` |
| CON           | `These results` | `[do] not`     | `support`   | `the use of HCQ in patients hospitalised for documented SARS-CoV-2-positive hypoxic pneumonia` |

**Table1:** Structure of disagreement sentences.



In case there are both a SUPPORT-verb and an EVIDENCE-noun in a sentence the computer ought predict it as being a disagreement sentence. Pairing an affirmative disagreement sentence (one without a negation expression) together with a negated disagreement sentence will constitute a disagreement pair.

But not all pairs of affirmative and negated disagreement sentences will make a disagreement pair. An example:

>(a) "The study shows that HCQ is effective against COVID-19"
>
>(b) "The study does not show that HCQ is effective against COVID-19"
>
>
>
>(c) "The study shows that climate change is caused by humans"
>
>(b) "The study does not show that HCQ is effective against COVID-19"

The sentence pair (a, b) is a disagreement pair whereas the pair (c, b) is not. This is because (a) and (b) are about the same subject whereas (c) and (b) are about different topics. Disagreement pairs consist of sentences that are about the same topic; one sentence is affirmative and the other is negated. To let the program decide whether two sentences are about the same topic I will make use of `spacy`'s functionality to compute the similarity of linguistic objects. The similarity can be computed for the whole sentence (or document) (which is called *Doc similarity*); or it can be computed for parts of the sentence (*Span similarity*). I take as the Span the part of the sentence which expresses its topic. The Span as understood here points to the statement of the sentence ($s$). $s$ is that what is supported by some kind of evidence. I assume that the Span is the part of the sentence that comes behind the SUPPORT-verb. (This is reflected in table 1 above.) As you can see from the table below the Span similarity is higher for similar topics and lower for different topics — as compared to Doc similarity:

| Sentence pair                                                | Doc similarity | Span similarity | Spans of sentence pair                                       |
| ------------------------------------------------------------ | -------------- | --------------- | ------------------------------------------------------------ |
| (a) "The study shows that HCQ is effective against COVID-19"<br /><br />(b) "The study does not show that HCQ is effective against COVID-19" | 0.98           | 1.0             | (Span a)  "that HCQ is effective against COVID-19" <br /><br />(Span b)  "that HCQ is effective against COVID-19" |
| (c) "The study shows that climate change is caused by humans"<br /><br />(b) "The study does not show that HCQ is effective against COVID-19" | 0.89           | 0.77            | (Span c)  "that climate change is caused by humans"<br /><br />(Span b)  "that HCQ is effective against COVID-19" |

**Table 2:** Comparison of Doc similarity and Span similarity.



For this project I will assume that Span similarity yields more reliable results than Doc similarity in determining the similarity of topic for two sentences. You could set a threshold for Span similarity, say 0.85. If the similarity for a pair of sentences is as high or higher as this threshold it ought to count as being about the same topic. So in this case the sentence pair (a, b) has a Span similarity which is greater than 0.85 (1.0 > 0.85). Thus the sentence pair counts as being about the same topic. And since (a) is affirmative and (b) is negated (a, b) is a disagreement pair. The pair (c, b) has a Span similarity below the threshold and therefore it does not count as a disagreement pair. I may now sum up the foregoing remarks in a hypothesis:

**Hypothesis**: Disagreement sentences express a relation that holds or does not hold between some kind of evidence and a statement (i. e. that the evidence supports/does not support some statement). That means disagreement sentences contain expressions referring to evidence and to the SUPPORT-relation. Furthermore disagreement pairs consists of one affirmative and one negated sentence that are about a similar topic (i. e. they show a certain degree (at least) of Span similarity). 



## Implementation: the Notebook

The task is to write a program such that the computer finds disagreement pairs from abstracts of scientific articles. (I follow a rule based approach, not one that involves machine learning.) In short, the following steps are taken: (i) get abstracts of scientific articles in a form that can be processed by the computer; (ii) divide abstracts into single sentences; (iii) filter disagreement sentences from the whole collection of sentences; (iv) label entities in disagreement sentences; (v) separate (entity labeled) disagreement sentences into two groups: affirmative sentences and negated sentences; (vi) make pairs of sentences with one affirmative and one negated sentence; (vii) determine Span similarity for each pair and — based on the Span similarity — distinguish disagreement pairs from non disagreement pairs.

* The first step was to get the abstracts of scientific articles in a form that the computer could read and process. I assessed the abstracts, prepared them and stored the results in the json-file `"HCQ_clean_abstracts.json"` (`"data/HCQ_clean_abstracts.json"`). This preparatory work was done in the additional notebook `"hcq_1_clean_abstracts.ipynb"`. (See [hcq_1_clean_abstracts.md](additional_notebooks/hcq_1_clean_abstracts.md); all additional notebooks together with commenting markdown-files can be found in the directory `"additional_notebooks"`.)


The other main steps are executed in the  notebook `"hcq_expressing_disagreement_in_abstracts.ipynb"`: 

* I loaded the the abstracts from `"HCQ_clean_abstracts.json"` into the **`dataframe`** `abstract_df`. There are the title of publication and an identifier assigned to each abstract. (Section 2 in the notebook; for more information see: [hcq_2_sentences.md](additional_notebooks/hcq_2_sentences.md).)

* Each abstract is divided into its sentences. I stored the sentences in another dataframe called `sentences_df`. Each sentence has a unique identifier and there is also the title of publication added from which the sentence was taken — in case these informations are needed (see [hcq_2_sentences.md](additional_notebooks/hcq_2_sentences.md)). I take the sentences from column `"sentence"` and convert them into **`Doc`**-objects which I store in the **`list`** `doc_list`.  Our sentence collection encompasses 216 sentences.(Section 3)

* We are now going to filter the sentences in `doc_list`. In order to do that we will use `spacy`'s **`Matcher`**. First we will filter the sentences by occurrence of SUPPORT-verbs. We are searching for particular verbs that are mostly the root of a sentence. Sometimes they are not the root but what is called in `spacy` an "open clausal complement" (`"xcomp"`). Thus the patterns for the Matcher have the following general forms:

  ```python
  [{"POS": "VERB", "DEP": "ROOT", "LEMMA": {"IN": verbs}}]
  [{"POS": "VERB", "DEP": "xcomp", "LEMMA": {"IN": verbs}}]
  ```

  I have manually selected some verbs that in my view indicate the relation SUPPORT (for more information see: [hcq_3_verb_filter.md](additional_notebooks/hcq_3_verb_filter.md)). The verbs in their basic form (*lemma*) are: "confirm", "reveal", "show", "suggest" and "support". The filtered sentences are stored in the list `verb_filtered_sentences`. (Subsection 4.1)

* We will further filter the remaining sentences in `verb_filtered_sentences` — this time by occurrence of EVIDENCE-nouns. These are particular nouns (including the pronoun "we") that most of the time serve as the nominal subject (`"nsubj"`) of a sentence (sometimes as an object (`"pobj"`). The Matcher searches with the general patterns:

  ```python
  [{"POS": "NOUN", "DEP": "nsubj", "LEMMA": noun/pronoun}]
  [{"POS": "NOUN", "DEP": "pobj", "LEMMA": noun}]
  ```

  I manually selected the nouns (for more information see: [hcq_4_noun_filter.md](additional_notebooks/hcq_4_noun_filter.md)). The nouns/pronouns are in the lemma form: "analysis", "evidence", "finding", "result", "survey", "trial" and "we". The further filtered sentences (datatype **`string`**) are stored in the list `noun_filtered_sentences`. From 216 sentences in our original collection 11 remained after filtering by SUPPORT-verbs and nine remained after filtering by EVIDENCE-nouns. (Subsection 4.2)

  I have tested a few different filters to assess which works best. The combination of filtering sentences by SUPPORT-verbs **and** by EVIDENCE-nouns yielded the best results: Out of 216 sentences 211 are predicted correctly. Only 64 % of disagreement sentences are found. But if the computer predicts a sentence as being a disagreement sentence you can be sure that this is correct. (For information on evaluation of filters see: [hcq_2_sentences.md](additional_notebooks/hcq_2_sentences.md), [hcq_3_verb_filter.md](additional_notebooks/hcq_3_verb_filter.md), [hcq_4_noun_filter.md](additional_notebooks/hcq_4_noun_filter.md).)

* Then I define entities that shall be labeled if expressions that refer to them occur in a sentence. Such expressions will be marked and highlighted. The entities are: SUPPORT (**`SUPP`**); evidence, processes resulting in evidence (**`EVID`**) or scientists/scientific institutions producing such evidence (**`SCI`**); and negations (**`NEG`**). To label entities I have to process the sentences in `noun_filtered_sentences`; in other words: strings have to be converted into Doc-objects. The resulting list of sentences (Doc-objects) is called `disagreement_sentences`. (Section 5; for more information see: [hcq_5_entity_labels.md](additional_notebooks/hcq_5_entity_labels.md).)

* The sentences in `disagreement_sentences` are divided into affirmative sentences and negated sentences. The affirmative sentences I store in `sents` and the negated sentences I store in `negated_sents`. (Subsection 6.1)

* Sentences from `sents` and `negated_sents` are paired. For each pair consisting of an affirmative and one negated sentence: The Spans after the SUPPORT-verb are taken and the Span similarity is computed. If the similarity is at least 0.83 the pair is stored in `disagreementPairs`. This list contains all found disagreement pairs. (Subsection 6.2)

  I have evaluated several similarity thresholds to find the one for which most disagreement pairs are predicted correctly. A threshold of 0.83 yielded the best results. 13 sentence pairs out of 20 are predicted correctly. The computer will find 75 % of all disagreement pairs. If a pair is predicted as being a disagreement pair this prediction is correct in 69 % of cases. (For more information see: [hcq_6_disagreement_pairs.md](additional_notebooks/hcq_6_disagreement_pairs.md).)



## Concluding Remarks

You can see the disagreement pairs at the end of Subsection 6.2; labeled entities are highlighted. The program forms disagreement pairs and many of them seem to be correct. But the program still has flaws. To name only the most important ones:

* The assumption that disagreement pairs show a higher Span similarity could not be confirmed. The average Span similarity of all considered sentence pairs is about 0.8331. The average Span similarity of sentences that I take to be disagreement pairs is very slightly higher: it is about 0.8332. With a value of about 0.8329 the average Span similarity of non disagreement pairs is only very slightly below the average Span similarity of all pairs. You could see it as pointing out to a trend but there is hardly any difference. What is more is that one disagreement pair shows a Span similarity of about 0.69 whereas a non disagreement pair shows as Span similarity of almost 0.89. ([hcq_6_disagreement_pairs.md](additional_notebooks/hcq_6_disagreement_pairs.md))

  I still believe, the assumption that disagreement pairs are about the same topic and non disagreement pairs are not, is correct. But there has to be a better way to assess topic similarity than by Span similarity.

* One sentence was correctly categorized as a negated disagreement sentence, but I think it was by mere accident. The negation did not negate the SUPPORT-verb but another verb in the "topic Span".



------

Frances, Bryan/Matheson, Jonathan (2019): "Disagreement", in: Zalta, Edward N. (Hrsg.): *The Stanford Encyclopedia of Philosophy* (Winter 2019 Edition), URL=[https://plato.stanford.edu/archives/win2019/entries/disagreement/](https://plato.stanford.edu/archives/win2019/entries/disagreement/).