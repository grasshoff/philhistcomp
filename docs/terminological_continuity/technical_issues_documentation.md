## A Computational Approach to the History of Philosophical Ideas - Documentation of Technical Issues (to be solved later on)

1. **OCR-preparation**

  * done


2. **OCR: Tesseract**

   1. fine-tuning psm?
     * esp. psm= 1 (autmatic page seg + OSD)
     * and psm= 3 (fully automatic, default)
   2. fine-tuning def. of "low word accuracy": 50, 60, 70?
   3. fine-tuning oem? (*engine modes* with different performance and speed)
     * 0 = Legacy engine only
     * 1 = Neural nets LSTM engine only
     * 2 = Legacy + LSTM engines
     * 3 = Default, based on what is available
   4. eliminate page-/footnote-*number* by defining white- or blacklist?
     * result not as imagined but blacklist can be used to prevent common OCR-mistakes, e.g. mistaking "I" (pronoun 1st person singular) for "[" or "|"
   5. eliminate headers by defining book-layout-specific spacy Matcher (or RE), then identify, replace, and delete?
   6. last-sent-on-page-problem for spacy sentenciser
     * possible (part of) solution: preserve_interword_spaces = 1
     * solution requires exact def. of:
       * last-sent-on-page-problem: occurs when (i) last sent. in (last) paragraph not ending with punctuation ('.', '!', '?'), and (ii) first sent. in next paragraph (i.e. first paragraph on next page) not starting with upper case letter (in short: defined by missing punctuation and lower case)
       *  headers: first paragraph on a page, spatially separated from main text, usually not longer than x words/characters, missing punctuation
       *  footers: paragraphs starting with a number, last (spatially separated) paragraph(s) on page
    * alternative solution: use computer vision (AI/ML) to recognise/analyse page layout, incl. headers & footnotes, e.g. Luminoth, DetectronV2, Larex, OCRopus, OpenCV
  7. maybe descrew some images (esp. the books/chapters I scanned myself) to improve quality of OCR-output


3. **SpaCy: preparation-notebook**

   1. how to pick all sentences in which SpaCy identified occurence of term soul and enumerate them in a dataframe
     - see, e.g., cell 142ff. in notebook "preparation_sample_DeAnima": for now, I have to (i) repeat command for every single match, (ii) copy those sentences into a new txt-file, and (iii) finally put them into a dataframe (for step iii, see notebook "analysis_sample_DeAnima")
   2. how to (automatically) create jsonl-file containing all of those sentences and relevant metadata
     - see notebook "prodigy_training_preparation.ipynb" as well as file "prodigy_input_data_sample.jsonl"
     - maybe already add relevant metadata to pdf-files in 'pre-editing' of OCR-preparation, i.e. while cutting out irrelevant material? But is the metadata going to be transfered to the txt-files?


4. **Prodigy: specialised NER-model**

  1. really necessary to create match patterns, i.e. create a phrase list using a word vectors model that includes vectors for multi-word-epxressions
     * "step 0" in Ines' youtube-tutorial
     * step 1, as explained on website (and what I've already tried out), only starts at 9min,30sec
     * see website-tutorial, sect. "Manual annotation with patterns", I think this is what Ines is doing in the video..
     * sum up: there are several different methods for trainaing a model for NER: (i) fully manual annotation, (ii) manual annotation with (token or string) patterns, (iii) manual annotation with suggestions from a model (ner.correct), (iv) binary annotation with active learning and a model in the loop (ner.teach), which can also used to improve accuracy of a new model after it has been trained on some data from scratch
  2. open (technical) questions for tomorrow: how to save trained model and cont. on next day, how to export model to use in spacy, what exactly is the gold-standard-data and what comes next? binary with trained model in loop (ner.teach) OR training experiments (train-recipe) OR batch-train?
  3. working order:
     1. train a model from scratch with ner.manual,
     2. run training experiments to see if it's going into the right direction: train-recipe (wrapper around spaCy’s training API) = ner.batch-train?
     3. optional: to improve accuracy, use ner.teach for binary annotation with active learning and a model in the loop
     4. last step: train the model with spaCy directly: data-to-spacy command converts Prodigy datasets to spaCy’s JSON format to use with the spacy-train-command
  4. distinction normal training / batch training: "the model in the loop is only updated once each new annotation. This is never going to be as effective as batch training a model on the whole dataset... If you batch train your model with the collected annotations afterwards, you should receive the same model you had in the loop, just better." 


user comment:

What I did to make the training of my NER Gold Faster was to first do a normal train for the new entities with some examples, then do a batch train and export the model. Then I used that model to annotate the GOLD.

I did few 100 examples of GOLD, exported it, used it to train a model with spacy batch train example, by loading the model I used to do the GOLD. Doing this I improved the model, and I did again some 100 examples of GOLD with the newly created model. The suggestion are then getting better and better and you have to do few corrections. I did this a few time.
