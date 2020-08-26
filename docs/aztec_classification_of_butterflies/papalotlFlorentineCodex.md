
# The butterflies of the Florentine Codex


```python
import pandas as pd
import re
import spacy

from spacy.matcher import PhraseMatcher
from spacy.matcher import Matcher
from spacy import displacy

from spacy.tokens import Span
```


```python
papalotl = open('papalotl.txt')
butterflies = papalotl.read()
```


```python
from spacy.lang.en import English

raw_text = butterflies
nlp = English()
nlp.add_pipe(nlp.create_pipe('sentencizer'))
doc = nlp(raw_text)
sentences = [sent.string.strip() for sent in doc.sents]
```


```python
# Get rid of newlines
sentences = [item.replace('\n', " ") for item in sentences]
```


```python
df = pd.DataFrame(sentences)
df.rename(columns={0: "Butterflies"})[1:5]
```




<div>
<style scoped>
    .dataframe tbody tr th:only-of-type {
        vertical-align: middle;
    }

    .dataframe tbody tr th {
        vertical-align: top;
    }

    .dataframe thead th {
        text-align: right;
    }
</style>
<table border="1" class="dataframe">
  <thead>
    <tr style="text-align: right;">
      <th></th>
      <th>Butterflies</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>1</th>
      <td>lt is fuzzy, like fat; winged.</td>
    </tr>
    <tr>
      <th>2</th>
      <td>Its wings are twofold.</td>
    </tr>
    <tr>
      <th>3</th>
      <td>It has arms, it has legs, it has antennae.</td>
    </tr>
    <tr>
      <th>4</th>
      <td>lt is a flyer, a constant flyer, a flutterer, ...</td>
    </tr>
  </tbody>
</table>
</div>




```python
nlp = spacy.load('en_core_web_lg')
```


```python
doc = nlp(butterflies)
```


```python
matcher = PhraseMatcher(nlp.vocab)
papalotl = nlp(butterflies)
phrase_list = ['abdomen', 'neck', 'wings', 'arms', 'legs', 'antennae']
phrase_patterns = [nlp(text) for text in phrase_list]
matcher.add('mariposa', None, *phrase_patterns)
found_matches = matcher(papalotl)
```


```python
for match_id, start, end in found_matches:
    string_id = nlp.vocab.strings[match_id]
    span = papalotl[start:end]
    print(start, end, span.text)
```

    15 16 abdomen
    20 21 neck
    34 35 wings
    40 41 arms
    44 45 legs
    48 49 antennae
    85 86 wings
    234 235 wings
    369 370 wings
    516 517 wings
    


```python
def show_ents(doc):
    if doc.ents: 
        for ent in doc.ents:
            print(ent.text+' - '+ent.label_)
    else:
        print('No entity found')
```


```python
ORGAN = doc.vocab.strings['ORGAN']
```


```python
new_ent = Span(doc, 15, 16, label=ORGAN)
new_ent1 = Span(doc, 20, 21, label=ORGAN) 
new_ent2 = Span(doc, 34, 35, label=ORGAN) 
new_ent3 = Span(doc, 40, 41, label=ORGAN) 
new_ent4 = Span(doc, 44, 45, label=ORGAN) 
new_ent5 = Span(doc, 48, 49, label=ORGAN) 
new_ent6 = Span(doc, 85, 86, label=ORGAN) 
new_ent7 = Span(doc, 234, 235, label=ORGAN) 
new_ent8 = Span(doc, 369, 370, label=ORGAN) 
new_ent9 = Span(doc, 516, 517, label=ORGAN) 
```


```python
doc.ents = list(doc.ents)+[new_ent]+[new_ent1]+[new_ent2]+[new_ent3]+[new_ent4]+[new_ent5]+[new_ent6]+[new_ent7]+[new_ent8]+[new_ent9]
```


```python
show_ents(doc)
```

    abdomen - ORGAN
    neck - ORGAN
    wings - ORGAN
    arms - ORGAN
    legs - ORGAN
    antennae - ORGAN
    wings - ORGAN
    XICALPAPALOTL - ORG
    XICALTECONPAPALOTL - ORG
    XICALTECON - PERSON
    xicalli - PERSON
    TLILPAPALOTL - PERSON
    wings - ORGAN
    TLECOCOZPAPALOTL - ORG
    quappachpapalotl - PERSON
    lts - PERSON
    IZTAC PAPALOTL
     - PRODUCT
    CHIAN PAPALOTL - PRODUCT
    wings - ORGAN
    TEXOPAPALOTL
     - LAW
    XOCHIPAPALOTL - PERSON
    UAPPAPALOTL - PERSON
    wings - ORGAN
    


```python
colors = {'ORGAN': 'purple'}
options = {'ents': ['ORGAN'], 'colors': colors}
```


```python
displacy.render(doc, style='ent', options=options)
```


<span class="tex2jax_ignore"><div class="entities" style="line-height: 2.5; direction: ltr">BUTTERFLY</br>Whatever the kind of butterfly, it is long and straight, the 
<mark class="entity" style="background: purple; padding: 0.45em 0.6em; margin: 0 0.25em; line-height: 1; border-radius: 0.35em;">
    abdomen
    <span style="font-size: 0.8em; font-weight: bold; line-height: 1; border-radius: 0.35em; text-transform: uppercase; vertical-align: middle; margin-left: 0.5rem">ORGAN</span>
</mark>
 is slender, the 
<mark class="entity" style="background: purple; padding: 0.45em 0.6em; margin: 0 0.25em; line-height: 1; border-radius: 0.35em;">
    neck
    <span style="font-size: 0.8em; font-weight: bold; line-height: 1; border-radius: 0.35em; text-transform: uppercase; vertical-align: middle; margin-left: 0.5rem">ORGAN</span>
</mark>
 is constricted. lt is fuzzy, like fat; winged. Its 
<mark class="entity" style="background: purple; padding: 0.45em 0.6em; margin: 0 0.25em; line-height: 1; border-radius: 0.35em;">
    wings
    <span style="font-size: 0.8em; font-weight: bold; line-height: 1; border-radius: 0.35em; text-transform: uppercase; vertical-align: middle; margin-left: 0.5rem">ORGAN</span>
</mark>
 are twofold. It has 
<mark class="entity" style="background: purple; padding: 0.45em 0.6em; margin: 0 0.25em; line-height: 1; border-radius: 0.35em;">
    arms
    <span style="font-size: 0.8em; font-weight: bold; line-height: 1; border-radius: 0.35em; text-transform: uppercase; vertical-align: middle; margin-left: 0.5rem">ORGAN</span>
</mark>
, it has 
<mark class="entity" style="background: purple; padding: 0.45em 0.6em; margin: 0 0.25em; line-height: 1; border-radius: 0.35em;">
    legs
    <span style="font-size: 0.8em; font-weight: bold; line-height: 1; border-radius: 0.35em; text-transform: uppercase; vertical-align: middle; margin-left: 0.5rem">ORGAN</span>
</mark>
, it has 
<mark class="entity" style="background: purple; padding: 0.45em 0.6em; margin: 0 0.25em; line-height: 1; border-radius: 0.35em;">
    antennae
    <span style="font-size: 0.8em; font-weight: bold; line-height: 1; border-radius: 0.35em; text-transform: uppercase; vertical-align: middle; margin-left: 0.5rem">ORGAN</span>
</mark>
. lt is a flyer, a constant flyer, a flutterer, a sucker of the different flowers, and a sucker of liquid. It is fuzzy. It trembles, it beats its 
<mark class="entity" style="background: purple; padding: 0.45em 0.6em; margin: 0 0.25em; line-height: 1; border-radius: 0.35em;">
    wings
    <span style="font-size: 0.8em; font-weight: bold; line-height: 1; border-radius: 0.35em; text-transform: uppercase; vertical-align: middle; margin-left: 0.5rem">ORGAN</span>
</mark>
 together, it constantly flies. It sucks, it sucks liquid. It is not solid. There are many kinds of butterflies.</br></br>XICALPAPALOTL OR XICALTECONPAPALOTL. OR XICALTECON</br>It is somewhat large. Its name comes from xicalli [gourd bowl] and papalotl [butterfly ], because it is yellow, it is quite yellow, it is fuzzy . And it is painted with black; it is varicolored. So it is very beautiful, coveted, desirable, constantly desirable, constantly required. It is fragile. It yellows, it becomcs painted, it becomes varicolored. It breeds, it becomes firm, it develops; it flies constantly.</br></br>TLILPAPALOTL</br>It is similar to - also just the same as - the xicalpapalotl, but black flecked with white. Thus its 
<mark class="entity" style="background: purple; padding: 0.45em 0.6em; margin: 0 0.25em; line-height: 1; border-radius: 0.35em;">
    wings
    <span style="font-size: 0.8em; font-weight: bold; line-height: 1; border-radius: 0.35em; text-transform: uppercase; vertical-align: middle; margin-left: 0.5rem">ORGAN</span>
</mark>
 are varicolored.</br></br>TLECOCOZPAPALOTL</br>Its name is also quappachpapalotl. It is called &quot;fireyellow&quot; because it glows, it glistens a little. lts body is a little fiery, but it is smoky, tawny. It glows, glistens, glistens constantly. It is tawny, smoky, smoky yellow. It becomes smoky yellow; it turns smoky yellow.</br></br>IZTAC PAPALOTL
Some are a little large, some average, some quite small. They are not really white, only as whitish, only as pale as they are yellow.</br></br>CHIAN PAPALOTL.</br>It is somewhat similar to the xicalpapalotl, because it is painted as if sprinkled with chia, flecked everywhere on its body; and its 
<mark class="entity" style="background: purple; padding: 0.45em 0.6em; margin: 0 0.25em; line-height: 1; border-radius: 0.35em;">
    wings
    <span style="font-size: 0.8em; font-weight: bold; line-height: 1; border-radius: 0.35em; text-transform: uppercase; vertical-align: middle; margin-left: 0.5rem">ORGAN</span>
</mark>
 are painted, painted with a chia design.</br></br>TEXOPAPALOTL
Some are large, some tiny; they are not uniform. Its name comes from texotli [light blue]. But it is not really a blue; it is just so rather pale, just so pallid,</br>just so yellow as to be livid. It is just a light blue hue. It takes on a light blue hue; it becomes blue-brown.</br></br>XOCHIPAPALOTL</br>Some are large, some small. Many kinds of colors are on them, that they are varicolored, much like flowers, of very intricate design, and truly sought after truly wonderful. They are o intricate design, sought after, flower-like.</br></br>UAPPAPALOTL</br>It is of average size; its 
<mark class="entity" style="background: purple; padding: 0.45em 0.6em; margin: 0 0.25em; line-height: 1; border-radius: 0.35em;">
    wings
    <span style="font-size: 0.8em; font-weight: bold; line-height: 1; border-radius: 0.35em; text-transform: uppercase; vertical-align: middle; margin-left: 0.5rem">ORGAN</span>
</mark>
 are painted chili-red. It is also beautiful, also wonderful. Amaranth leaves or foliage are also called uappapalotl when the amaranth leaves already become mature, ripe. Then they are named uappapalotl, because they become yellow or chili-red.</div></span>

