## A Computational Approach to the History of Philosophical Ideas - Documentation of Technical Issues (to be solved later on)

1. **OCR-preparation**

   1. for now two versions (i.e. notebooks), one in which automated renaming of output files (images) works, one in which output is directly saved to designated folder. Desired result: both correct naming and saved in designated folder
   2. pre-editing: cut irrelevant material (i.e. entire pages: (i) for pdfs: cut intro, toc, index etc.; (ii) for doc (De Anima): in add. to (i), also cut footnotes, page numbers etc.), for now by using pdf24 (external, manual). Easier with Python? I guess there is no way of 'automising' this preliminary step (as different books come with different kinds/amounts of additional material)?


2. **OCR: Tesseract**

   1. *page segmentation mode* for double pages (book format)? psm=1 vs psm=3 (default). result: psm=1 not more accurate and even a bit slower than psm=default (repeat comparison on bigger sample to verify results)
   2. OCR works fine for main text but problems with footnotes/footers/headers. Possible solution: eliminating sentences containing words with low word confidence (e.g. less than 50%), hence footnotes will not be taken into account in subsequent NLP (prodigy, spacy).
    - Desired result of 'post-editing': get one coherent txt-file  containing text from all images in correct order (i.e. ocr'ed output of individual images merged into one txt-file, excluding sentences with low word confidence, i.e. get rid of footnotes, headers, page numbers etc.), thus ready for subsequent NLP.
    - Problem: how to (automatically) identify footnotes if number of fn (e.g. footnote "20") is not always ocr'ed as such? (but, e.g., as special character). Eliminating *all* sentences with low word confidence = risk of eliminating important sentences...
    - try to come up with a different soulution (solely relying on elimination of sentences with low word confidence seems problematic..)



3. **specialised NER-model: Prodigy**

   1. how to pick all sentences in which SpaCy identified occurence of term soul and enumerate them in a txt-file *and/or* how to (automatically) create jsonl-file containing all of those sentences and relevant metadata (as example, see file "prodigy_input_data_sample.jsonl")
