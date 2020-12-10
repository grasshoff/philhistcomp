# ** Water security and water insecurity**

### Background

Human beings need water to survive. The access to water is one of the Sustainable Development Goals (SDG) which were set by the United Nations General Assembly. In 2015 they formulated 17 goals which are supposed to be achieved by 2030. Number Six is "Clean Water and Sanitation". Closely related to the sixth SDG is the term "Water security". The term is used in different areas today like Engineering, Geography and Environmental Science. The meaning of the term is part of the project. A broad definition is that a city or region is water secure if the water-related risk to society is tolerable. That means water access is reliable, the water quality and quantity are acceptable and the risk of floods is low. The opposite of it is called "water insecurity". 

### **Project motivation**

Water security and insecurity are supposed to be the opposite. What is secure, cannot be insecure. What is insecure, cannot be secure. My observation is that the terms aren't used as exact opposites. My impression from other research projects is that water insecurity has a different connotation that water security. I think that the meaning of the first one is closer related to social problems which cause water insecurity, the second one is closer related to technological solutions which establish or maintain water security. 

----

### **Research question**

This project investigates the meanings of water security and water insecurity. The goal is to find out if they are used as the opposite as one would expect with security and insecurity.


### **Research object**

**research objects** = All scientific publications which use the "water security" or "water insecurity" until 14th of May in 2020

### **Empirical source: ** https://app.dimensions.ai/discover/publication?search_text=Water%20security&search_type=kws&search_field=full_search bzw. https://app.dimensions.ai/discover/publication?search_text=Water%20insecurity%20&search_type=kws&search_field=full_search


---

### **Method**

The data retrieved from dimensions.ai was prepared and analyzed in the attached Jupyter notebook. A data frame was created and natural language processing (NLP) was applied. One part of the program scraped the sentences from the abstracts. The number of abstracts are 2881 and the number of sentences are 23176. The important tool for the analysis is the open-scoure software spaCy. It is developed for advances NLP. It cannot be guaranteed that all of the abstracts are in English but it can be assumed that most of them are. Therefore, the English library en_core_web_lg got loaded. The key function in the analysis is spaCy's noun chunks. Noun chunks compare the similarity of a certain key word with other words. If two words are used in the exact same contexts, they seem to be very similar. Every word is given a value between zero and one. If the similarity of the word and the key word is lower, the assigned value is lower as well. Therefore, noun chunks assigns a value close to one, if the key word is "water security" and the word is "security". 

### **The 100 words which describe water security and water insecurity the most**

The two lists "security_100" and "insecurity_100" include the 100 words which have the highest value close to one. The words security and insecurity are sorted out. After clustering similar words, the output looks like this:

water security:
water safety, water systems, water (resources) protection, water policy, water infrastructure, water access, water management,

water insecurity:
water poverty, water scarcity, water vulnerability, water crisis, water conflicts, water shortage, water mismanagment, water inequality

Some words which describe security have the opposite meaning of the words which describe the insecurity. Water managment and mismanagment is the best example. Water protection is the opposite of water vulnerability. Water access and safety are linked to fulfilling the sixth SDG, water crisis, shortage and scarcity are not. This finding does not support the hypothesis.
However, water inequality is an interesting finding. It doesn't have a counterpart on the water security's side. It is a term which indicates a social injustice. 

Redoing the process without the first 100 words which have been found in the first round was unsuccessful.

### **Comparing the similarity**

This is a new approach. Before the similarity was used independently for both terms. Now the similarity values for one word and two key words gets connected and compared. It leads to the differences between the two key words. Which word is very similar to water security but less similar to water insecurity? 



----

### **Preliminary results**


____


#### Outlook

----

### **Footnotes**

[^1] 

---

### **Sources**

