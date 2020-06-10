### Terminological Continuity between Medieval and Early Modern Philosophy - (possible) strategy for first comparison

1. **Choosing two sample texts**

    * Sydney Penner's (2011) English translation of Suárez' *De Anima, Disputation 12, Question 2* (cf. pdf-file in subfolder "empirical sources")
    * Elizabeth Haldane's (1911) English translation of Descartes' *Meditationes*
    * choosing a (philosophical) concept whose usage in the two sample texts is to be analysed: *soul*


2. **Preparing the txt-files for text analysis**

    * cleaning up the txt-file: removing all footnotes, page numbers, etc.; re-separating words that stuck together as a result of transforming the original pdf-file into a txt-file
    * import txt-file into jupyter notebook; specify encoding: *DeAnima = open('DeAnima.txt', encoding="utf8")*
    * get rid of new lines, divide into sentences using *nlp.create_pipe('sentencizer')*, and transform text into list of sentences using *df = pd.DataFrame(sentences)*


3. **Comparing the texts with SpaCy**

    * load statistical model for English: *nlp = spacy.load('en_core_web_lg')*
    * identify all sentences in which Suárez and Descartes mention the word (i.e. the token) "soul", using SpaCy Matcher: *pattern = [{'LEMMA' : 'soul'}]* (or alternatively by using the package *regular expressions*)
    * in those sentences, identify all adjectives and verbs which stand in dependence relation with word "soul" using *token.dep_*
    * create dataframe with adjectives and verbs used in connection with "soul" in Suárez and Descartes respectively (i.e. 2 columns). Alternatively: create two new doc-objects, each containing an enumeration of the adjectives and verbs in question
    * comparing semantic similarity between the two newly created doc-objects using *print(doc_DeAnima.similarity(doc_Meditationes))*
