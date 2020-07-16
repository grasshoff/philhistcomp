import spacy
from spacy.matcher import Matcher
import os.path
import io

data_folder = os.path.join("/Users/juangarciaberdoy/Documents/GitHub/philhistcomp/projects", "piraha_language")
file_to_open = os.path.join(data_folder, "corpus.txt")
ff = io.open(file_to_open, 'r', encoding='utf-8')

nlp = spacy.load("en_core_web_sm")
matcher = Matcher(nlp.vocab)

patterns = [[{"LOWER": "two"}], [{"LOWER": "one"}], [{"LOWER": "is"}], [{"LOWER": "plus"}]]

doc = nlp(ff.read())
for pattern in patterns:
    print(pattern)
    matcher.add("tempId", None, pattern)
    matches = matcher(doc)
    print(len(matches))
    matcher.remove("tempId")