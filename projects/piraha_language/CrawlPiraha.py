import spacy
from spacy.matcher import Matcher
import os.path
import io

data_folder = os.path.join("/Users/juangarciaberdoy/Documents/GitHub/philhistcomp/projects", "piraha_language")
file_to_open = os.path.join(data_folder, "corpus.txt")
ff = io.open(file_to_open, 'r', encoding='utf-8')

nlp = spacy.load("en_core_web_sm")
matcher = Matcher(nlp.vocab)

patterns = [[{"LOWER": "two"}], [{"LOWER": "one"}], [{"LOWER": "is"}], [{"LOWER": "and"}]]

doc = nlp(ff.read())
for pattern in patterns:
    print(pattern)
    matcher.add("HelloWorld", None, pattern)
    matches = matcher(doc)
    print(len(matches))
    span = doc[matches[0][1]:matches[0][2]]
    print(span.text)
    #span = doc[matches[0].start:matches[0].end]
    print("stop")
    #span = doc[start:end]
    '''
    for match_id, start, end in matches:
        string_id = nlp.vocab.strings[match_id]  # Get string representation
        span = doc[start:end]  # The matched span
        print(match_id, string_id, start, end, span.text)'''