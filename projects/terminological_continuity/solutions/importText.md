#### PHILOSOPHY AND HISTORY OF SCIENCE WITH COMPUTATIONAL MEANS

##### PROF. DR. GERD GRAßHOFF 



# Import text files

- see notebook "ImportTextSources.ipynb" in importSources
- keywords: pandas, import, text



## Import by sentences

Using a for loop and the sents function, we can detect a list of sentences.

```
temp = open(datei,'r').read().replace("\n"," ")
tokens=nlp(temp)
for i,s in enumerate(tokens.sents):
    sent[i]=s.text
sent[3]
```


Import the dictionary of sentences to a DataFrame.

```
dfDeAnimaSent.from_dict(sent,orient="index",columns=["satz"])
dfDeAnimaSent

```



To see the documentation of the DataFrame.from_dict() function from pandas, go to:

https://pandas.pydata.org/pandas-docs/stable/reference/api/pandas.DataFrame.from_dict.html