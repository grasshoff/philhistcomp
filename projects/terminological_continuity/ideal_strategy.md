### Terminological Continuity between Medieval and Early Modern Philosophy: *Ideal Strategy*

1. **Preparation 1 (before importing the txt-files into Jupyter Notebook)**

    1. find all the relevant writings in txt-format and in one coherent language (already done except for Ockham, probably in Latin as most parts of the writings haven't been translated into English yet. I will thus use the spaCy model for Latin, whose efficiency is uncertain.)
    2. merge all the relevant writings of the three authors, i.e. create 3 big txt-files, one for each author, containing all the relevant raw data: Descartes.txt, Suarez.txt, Ockham.txt
    3. clean up the txt-files: page numbers, footnotes, words sticking together etc. (ideally: write or find an algorithm to do the job for me)


2. **Preparation 2 (in Jupyter Notebook)**

    1. create doc-objects for each of the three txt-files, divide into sentences using "sentencizer", get rid of new lines
    2. create dataframe_A with 4 columns:
        1. sentence number
        2. enumeration of all sentences in doc_Descartes
        3. enumeration of all senteces in doc_Suarez
        4. enumeration of all senteces in doc_Ockham
    3. turn dataframe_A into json-file. This is the **first checkpoint**


3. **Preparatory Analysis**

    1. use SpaCy Matcher to define pattern = [{"LEMMA" : "soul" *and/or* "faculty"}]
    2. write algorithm to detect implicit references to the terms in question: *if* match acc. to pattern defined in 3.1, *then* check also subsequent sentence: if subject of the sentence = a pronoun, then count as match. If match, then check also subsequent sentence: ... (this is only a first formulation of a possible strategy for detecting implicit references, still requires some refinement)
    3. apply steps 3.1 and 3.2 to doc_Descartes, doc_Suarez, and doc_Ockham
    4. create dataframe_B with 4 columns for each author, i.e. 12 columns in total (creation of dataframe requires several steps, maybe divide into 2 separate dataframes, one for spans and sentence number, one for sentence number and corresponding sentence):
        1. span_start for matches in Descartes
        2. span_end for matches in Descartes
        3. sentence number (acc. to dataframe_A) in which span occurs
        4. text of corresponding sentence
        5.  span_start for matches in Suarez
        6. span_end for matches in Suarez
        7. sentence number (acc. to dataframe_A) in which span occurs
        8. text of corresponding sentence
        9.  span_start for matches in Ockham
        10. span_end for matches in Ockham
        11. sentence number (acc. to dataframe_A) in which span occurs
        12. text of corresponding sentence
    5. turn dataframe_B into json-file. This is the **second checkpoint**
    6. analyse linguistic features of the sentences in columns 4 (d), 8 (h), and  12 (l) using part-of-speech tag and dependency (maybe turn into 3 separate doc-objects, one for each author, in order to facilitate analysis)
    7. extract all adjectives and verbs that stand in direct relation with matches (i.e. with the terms "soul" and/or "faculty" as well as with the substituting pronouns). The underlying idea of step 7 is the following: since *all* remaining sentences are dealing with the concept of "soul" and/or "faculty", an analysis of their semantic similarity will yield a high degree of similarity, no matter *how* the three authors elaborate on those concepts. One should thus ignore the common feature of all those sentences (i.e. the term "soul" and/or "faculty") and only take into account the verbs and adjectives used in connection with those terms. This will allow for a more fine-grained semantic comparison. Important: include negation *as part of* the adjectives and verbs
    8. create dataframe_C with 2 columns for each author, i.e. 6 columns in total:
        1. all adjectives used in direct connection with "soul" and/or "faculty" in Descartes
        2. all verbs used in direct connection with "soul" and/or "faculty" in Descartes
        3. all adjectives used in direct connection with "soul" and/or "faculty" in Suarez
        4. all verbs used in direct connection with "soul" and/or "faculty" in Suarez
        5. all adjectives used in direct connection with "soul" and/or "faculty" in Ockham
        6. all verbs used in direct connection with "soul" and/or "faculty" in Ockham
    9. turn dataframe_C into json-file. This is the **third checkpoint** (and maybe turn again into three separate doc-objects, one for each author)


4. **Final Analysis**

    1. analyse semantic similarity between the three sets of adjectives and verbs:
        1.  doc_AdjVrbDescartes.similarity(doc_AdjVrbSuarez)
        2.  doc_AdjVrbDescartes.similarity(doc_AdjVrbOckham)
        3.  doc_AdjVrbSuarez.similarity(doc_AdjVrbOckham)
    2. compare degree of semantic similarity: if degree of similarity Descartes/Ockham > degree of similarity Suarez/Ockham, then this amounts to an empirical validation of Perler's thesis with respect to the progressive/evolutionary development from medieval to early modern philosophy which devaluates Descartes as the "grounding father"
