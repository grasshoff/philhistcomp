# Florentine Codex

## Background

In his ethnographic research, Bernardino de Sahagún obtained the help of two critical indigenous groups: the wise elders of numerous cities in central Mexico and the Nahua students of the Colegio de Santa Cruz de Tlatelolco (which was the first European school of higher learning in the Americas)

The Florentine Codex covers the most diverse aspects of the Aztec society, e.g., history, religion, and medical knowledge; it consists of twelve books.



#### Book XI: Earthly Things

Book XI, the longest in the codex, is a treatise on natural history. Following the traditional division of common knowledge in many European works, the Florentine Codex deals with everything divine, human, and natural of New Spain. Therefore, after having spoken of superior beings and human beings, Sahagún examines animals, plants, and all types of minerals.



#### Aztec Classification of Animals

Sahagún organized the work in two columns: on the right is the original text in Nahuatl and on the left, Sahagún's free translation into Spanish. 

"Papalotl" means "butterfly" and as we can appreciate from the example, there are different words that contain the name "papalotl": 

1. Tlilpapalotl (black-butterfly)
2. Tlecôzpapalotl (will-go-up-butterfly)
3. Iztapapalotl (white-butterfly)
4. Chianpapalotl (chia-seeds-butterfly)
5. Xicalpapalotl (gourd-butterfly)
6. Texopapalotl (blue-butterfly)
7. Xochipapalotl (flower-butterfly)
8. Vappapalotl (foliage-butterfly)

In this example, "papalotl" is the general term, and the eight kinds of butterflies registered are specific terms.



![papalotl](assets\papalotl.jpg)



#### Research Object

The English translation by Dibble and Anderson of the text of the butterflies on fo. 100f.

####  

#### Empirical Source

The source of this project is [Earthly Things](https://www.wdl.org/en/item/10622/view/1/200/), the penultimate book, and mainly, the goal is to analyze the butterflies' statements in the Florentine Codex. 



#### Research Question

What are the typical statements about butterflies in the Florentine Codex?



#### Method

1. To import the text.
2. To tokenize into sentences.
3. To create a Data Frame.
4. To train the category 'ORGAN.'
5. To visualize the newly named entity.
6. To search for most frequent words, verbs, and adjectives.



#### So far accomplished 

I successfully imported all the needed libraries (pandas, re, spaCy, PhraseMatcher, Matcher, displaCy, span). Then, I prepared a TXT file and read it into the Jupyter Notebook. The next steps were to split the text into sentences and to clean up the newlines. We present the sentences in a Data Frame. Next, we loaded the English model, and we make a 'doc.'  With the Phrase Matcher, we found the mentioned organs of the butterflies:

- abdomen
- neck
- wings
- arms
- legs
- antennae

We check where they span and add them to spaCy generating the label 'ORGAN.' A function helps to see if it worked. With displaCy, I rendered the result.



![schm](assets\schm.PNG)



#### Results

After some statistical functions, we found the typical verbs and adjectives used to describe the butterflies.

What do butterflies do according to the Aztec conception? 

- fly
- suck
- become
- develop
- turn
- glow
- glisten
- tremble

What are the descriptions used to characterize the butterflies in the Florentine Codex?

- twofold
- fuzzy
- whitish
- pale
- light-blue
- blue-brown
- fire-yellow
- flower-like
- beautiful
- desirable
- truly wonderful
- with a chia design
- of very intricate design

Which were the most frequent words? 

- yellow
- painted
- wings
- smoky
- constantly
- blue
- varicolored
- large

Other related nouns?

- size
- colors
- liquid
- hue
- flutterer



#### What could be improved?

1. I added the category of the organs manually. It would better to use prodigy to train the model, but I am still working on it.
2. All the other named entities I omitted because spaCy miscategorized some words. 
3. Apply this method to all the rest of the animals of the Florentine Codex.



## Conclusion

The Aztecs identified six organs of the butterflies, according to the Florentine Codex. Although they included the legs, we can't see them in the drawing. And they drew the proboscis but did not mention it in the text. They denominate the organs in the first paragraph and tell us that butterflies continuously fly and suck the liquid of flowers. 



![mariposa](assets\mariposa.PNG)



The next sections give details about the different kinds of butterflies, introducing all the different names. Always differentiating between color to name the different types of butterflies. Sahagún and his scholars refer to the change of tone of color that some butterflies show. Some kinds of butterflies have different sizes. They also give an account of the patterns of the wings. There are sentences about butterflies on the Florentine Codex that express that the Aztec thought that these insects were beautiful. Finally, we can conclude that most of the sentences tell us how the butterflies are. The rest of the sentences are about the actions of the butterflies.

Below we can see the essential characteristics of the kinds of butterflies.

1. Tlilpapalotl: black flecked with white
2. Tlecôzpapalotl: fire-yellow, it turns smoky yellow
3. Iztacpapalotl: different sizes, white
4. Chianpapalotl: chia design
5. Xicalpapalotl: large, yellow, it becomes varicolored
6. Texopapalotl: large and tiny, a light blue hue, it becomes blue-brown
7. Xochipapalotl: large and small, varicolored, intricate design
8. Vappapalotl: average size, chili-red



## References

Sahagún, Bernardino de. "General History of the Things of New Spain by Fray Bernardino de Sahagún: The Florentine Codex. Book II: The Ceremonies ". World Digital Library, June 2014, p. 200ff.

Sahagún, Bernardino de. *Florentine Codex: General History of the Things of New Spain: Book 11 - Earthly Things*. Translated by Charles E. Dibble and Arthur J. O. Anderson. Salt Lake City: The School of American Research; the University of Utah, 2012, p. 94f.





