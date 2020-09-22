## A Computational Approach to the History of Philosophical Ideas - Documentation of Technical Issues (to be solved later on)

1. **OCR-preparation: image2pdf**

   1. for now two versions (i.e. notebooks), one in which automated renaming of output files (images) works, one in which output is directly saved to designated folder. Desired result: both correct naming and saved in designated folder


2. **OCR: Tesseract**

   1. choosing right *page segmentation mode* for double pages (book format) if necessary. result: psm=1 not more accurate and even a bit slower than psm=default (repeat comparison on bigger sample to verify results)
   2. OCR works fine for main text but problems with footnotes/footers/headers. Possible solution: eliminating sentences containing words with low word confidence (e.g. less than 50%), hence footnotes will not be taken into account in subsequent NLP (prodigy, spacy). Desired result: get one coherent txt-file containing text from all images (excluding sentences with low word confidence, e.g. footnotes) in correct order, thus ready for subsequent NLP. problem: how to (automatically) identify footnotes if number of fn (e.g. footnote "20") is not always ocr'ed as such? (but, e.g., as special character). Eliminating *all* sentences with low word confidence = risk of eliminating important sentences...
   3. processing multipe double pages at once, i.e. processing all images in one directory (see test 5: 5.1 vs 5.2 vs 5.3)
   4. try out Latin model for OCR (even if it works, keep in mind further obstacles for subsequent NLP-analysis: SpaCy model for English probably much more powerful than Latin model)
   5. wrt original Latin texts: (a) research on Internet (one last time) for Latin texts in txt-format or as "searchable pdf", (b) if OCR works for Latin, download pdf-versions of scanned books available at various online archives (instead of scanning them by myself)


3. **specialised NER-model: Prodigy**
