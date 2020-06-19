### Terminological Continuity between Medieval and Early Modern Philosophy: *Ideal Strategy*

1. **Preparation 1 (before importing the txt-files into Jupyter Notebook)**

    1. find all the relevant writings in txt-format and in one coherent language (already done except for Ockham, probably in Latin as most parts of the writings haven't been translated into English yet. I will thus use the spaCy model for Latin, whose efficiency is uncertain.)
    2. merge all the relevant writings of the three authors, i.e. create 3 big txt-files, one for each author, containing all the relevant raw data: Descartes.txt, Suarez.txt, Ockham.txt
    3. clean up the txt-files: page numbers, footnotes, words sticking together etc. (ideally: write an algotithm to do the job for me)


2. **Preparation 2 (in Jupyter Notebook)**

    1. create doc-objects for each of the three txt-files, divide into sentences using "sentencizer", get rid of new lines
    2. create dataframe_A with 4 columns:
        1. sentence number
        2. enumeration of all senteces in doc_Descartes
        3. enumeration of all senteces in doc_Suarez
        4. enumeration of all senteces in doc_Ockham
    3. turn into json-file. This is the **first checkpoint**


3. **Preparatory Analysis**

    1. use SpaCy Matcher to define pattern = [{"LEMMA" : "soul" *and/or* "faculty"}]
    2. write algorithm to detect implicit references to the terms in question: *if* match acc. to pattern defined in 3.1, *then* check also subsequent sentence: if subject of the sentence = a pronoun, then count as match. If match, then check also subsequent sentence: ... (this is only a first formulation of a possible strategy for detecting implicit references, still requires substantial refinement)
    3. apply steps 3.1 and 3.2 to doc_Descartes, doc_Suarez, and doc_Ockham
    4. create dataframe_B with 4 columns for each author, i.e. 12 columns in total:
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
    5. analyse linguistic features of the sentences in columns 4, 8, and  12 using part-of-speech tag and dependency (maybe turn into 3 separate doc-objects in order to facilitate analysis); extract all adjectives and verbs that stand in direct relation with matches (i.e. with the terms "soul" and/or "faculty" as well as with the substituting pronouns), important: include negation *as part of* the adjectives and verbs
    6. create dataframe_C with 2 columns for each author, i.e. 6 columns in total
        1. ... 
