### Terminological Continuity between Medieval and Early Modern Philosophy - First Steps of Analysis

1. **Choosing a sample text**

    * Sydney Penner's (2011) English translation of Suárez' *De Anima, Disputation 12, Question 2* (cf. pdf-file in subfolder "empirical sources")
    * choosing a (philosophical) concept whose usage in the sample text is to be analysed: *soul*


2. **Preparing the txt-file for text analysis**

    * cleaning up the txt-file: removing all footnotes, page numbers, etc.; re-separating words that stuck together as a result of transforming the original pdf-file into a txt-file (this was still quite easy for the current sample text, however, for a large-scale analysis of Descartes', Suárez', and Ockham's writings, I will not be able to manually clean up hundreds or even thousands of pages of text...)
    * import txt-file into jupyter notebook; specify encoding: *DeAnima = open('DeAnima.txt', encoding="utf8")*
    * get rid of new lines, divide into sentences using *nlp.create_pipe('sentencizer')*, and transform text into list of sentences using *df = pd.DataFrame(sentences)*


3. **Analysing the text with SpaCy**

    * load statistical model for English: *nlp = spacy.load('en_core_web_sm')*
    * identify all sentences in which Suárez mentions the word (i.e. the token) "soul", using SpaCy Matcher: *pattern = [{'LEMMA' : 'soul'}]* (or alternatively by using the package *regular expressions*)
    * context-specific linguistic analysis (or rather: context-specific prediction of linguistic attributes) using *token.pos_, token.dep_, and ent.label_*
