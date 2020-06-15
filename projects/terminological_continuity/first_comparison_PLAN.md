### Terminological Continuity between Medieval and Early Modern Philosophy - strategy for first comparison

1. **Choosing two sample texts**

    * Sydney Penner's (2011) English translation of Suárez' *De Anima, Disputation 12, Question 2* (cf. pdf-file in subfolder "empirical sources")
    * Elizabeth Haldane's (1911) English translation of Descartes' *Meditationes*
    * choosing a (philosophical) concept whose usage in the two sample texts is to be analysed: *soul*


2. **Preparing the txt-files for text analysis**

    * cleaning up the txt-file: removing all footnotes, page numbers, etc.; re-separating words that stuck together as a result of transforming the original pdf-file into a txt-file
    * import txt-file into jupyter notebook; specify encoding: *DeAnima = open('DeAnima.txt', encoding="utf8")*
    * create doc-objects for the two txt-files, divide into sentences using *nlp.create_pipe('sentencizer')*, get rid of new lines
    * create dataframe_A with 3 columns:
        1. sentence number
        2. enumeration of all sentences in doc_Meditationes
        3. enumeration of all senteces in doc_DeAnima
    3. turn dataframe_A into json-file. This is the **first checkpoint**


3. **Comparing the texts with SpaCy**

    * load statistical model for English: *nlp = spacy.load('en_core_web_lg')*
    * identify all sentences in which Suárez and Descartes mention the word (i.e. the token) "soul", using SpaCy Matcher: *pattern = [{'LEMMA' : 'soul'}]* (or alternatively by using the package *regular expressions*)
    * create dataframe_B with 4 columns for each author, i.e. 8 columns in total (creation of dataframes requires several steps, maybe divide into 2 separate dataframes, one for spans and sentence number, one for sentence number and corresponding sentence):
        1. span_start for matches in Descartes
        2. span_end for matches in Descartes
        3. sentence number (acc. to dataframe_A) in which span occurs
        4. text of corresponding sentence
        5.  span_start for matches in Suarez
        6. span_end for matches in Suarez
        7. sentence number (acc. to dataframe_A) in which span occurs
        8. text of corresponding sentence
    * turn dataframe_B into json-file. This is the **second checkpoint**
    * analyse linguistic features of the sentences in columns 4 and 8 using part-of-speech tag and dependency (maybe turn into 2 separate doc-objects, one for each author, in order to facilitate analysis)
    * extract all adjectives and verbs that stand in direct relation with matches (i.e. with the terms "soul"). Important: include negation *as part of* the adjectives and verbs
    * create dataframe_C with 2 columns for each author, i.e. 4 columns in total:
        1. all adjectives used in direct connection with "soul" and/or "faculty" in Descartes
        2. all verbs used in direct connection with "soul" and/or "faculty" in Descartes
        3. all adjectives used in direct connection with "soul" and/or "faculty" in Suarez
        4. all verbs used in direct connection with "soul" and/or "faculty" in Suarez
    * turn dataframe_C into json-file. This is the **third checkpoint** (and turn again into two separate doc-objects, one for each author: doc_AdjVrbMeditationes, doc_AdjVrbDeAnima)
    * compare semantic similarity between the two newly created doc-objects using *print(doc_AdjVrbMeditationes.similarity(doc_AdjVrbDeAnima)*
