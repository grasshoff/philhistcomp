### Terminological Continuity between Medieval and Early Modern Philosophy - First Steps of Analysis

1. **Choosing a sample text**

    * Sydney Penner's (2011) English translation of Suárez' *De Anima, Disputation 12, Question 2* (cf. pdf-file in subfolder "empirical sources")
    * choosing a (philosophical) concept whose usage in the sample text is to be analysed: *soul*


2. **Preparing the txt-file for text analysis**

    * cleaning up the txt-file: removing all footnotes, page numbers, etc.; re-separating words that stuck together as a result of transforming the original pdf-file into a txt-file (this was still quite easy for the current sample text, however, for a large-scale analysis of Descartes', Suárez', and Ockham's writings, I will not be able to manually clean up hundreds or even thousands of pages of text...)
    * import txt-file into jupyter notebook; specify encoding: *DeAnima = open('DeAnima.txt', encoding="utf8")*
    * divide into sentences using *nlp.create_pipe('sentencizer')*, get rid of new lines
    * turn text into dataframe_A with 2 columns:
        1. sentence number
        2. text of corresponding sentence


3. **Analysing the text with SpaCy**

    * load statistical model for English: *nlp = spacy.load('en_core_web_sm')*
    * identify all sentences in which Suárez mentions the word (i.e. the token) "soul", using SpaCy Matcher: *pattern = [{'LEMMA' : 'soul'}]* (or alternatively by using the package *regular expressions*)
    * create dataframe_B with 4 columns:
      1. span_start for matches
      2. span_end for matches
      3. sentence number (acc. to dataframe_A) in which span occurs
      4. text of corresponding sentence
    * create new doc-object with all sentences in column 4 of dataframe_B
    * context-specific linguistic analysis (or rather: context-specific prediction of linguistic attributes) for sentences in newly created doc-object, using *token.pos_* and *token.dep_*
    * extract all adjectives and verbs that stand in direct connection with matches, i.e. with the term "soul"
    * create dataframe_C with 2 columns:
        1. all adjectives used in direct connection with "soul"
        2. all verbs used in direct connection with "soul"
