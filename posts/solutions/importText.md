---
keywords: pandas, import, text
---

# Import text files

- see notebook "ImportTextSources.ipynb" in importSources

## Import by sentences

```
temp = open(datei,'r').read().replace("\n"," ")
tokens=nlp(temp)
for i,s in enumerate(tokens.sents):
    sent[i]=s.text
sent[3]
```


Import the dictionary of sentences to a dataframe

```
dfDeAnimaSent.from_dict(sent,orient="index",columns=["satz"])
dfDeAnimaSent

```
