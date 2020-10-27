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
