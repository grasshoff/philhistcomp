# The female non-heroes in online "nurse hate threads"

### Regine Rørstad Torbjørnsen 

[image]

### Background

Since the beginning of the Covid-19 pandemic nurses (alongside doctors and other medical workers) have been celebrated globally for risking their health and working overtime in order to fight the virus. The work done by nurses has been subject to increased attention, which has led to a debate on their work conditions, wages, and the social recognition of their profession. 

The mainstream celebration of nurses has, however, also provoked hatred and frustration in certain sub-discourses. On the male dominated online imageboard [4chan](https://4chan.org) there has been a rise of discussion threads revolving around hatred against nurses. These threads mostly appear on the board /pol/ ("Politically Incorrect") and are referred to as "nurse hate threads". The vast majority of these discussions combine the hatred against nurses with explicit misogyny; the nurses are claimed to be incapable, immoral or immature in virtue of being women. Moreover, these negative characterizations of nurses seemingly aim at creating an antithesis to the figure of the hero. Nurses (predominantly in virtue of being women) are ridiculed as not skilled, ethical, mature, or smart enough to be real heroes. Rather, they are assumed to take advantage of the pandemic to stage themselves as heroes. 

The observations sketched above give rise to multiple questions. Why is the figure of a female hero so negatively received in certain online sub-discourses? Are such misogynist attitudes augmented in the context of a pandemic? The Covid-19 virus causes global fear and instability. Thus, there is reason to believe that the hatred against female nurses is partly driven by a notion of powerlessness and a fear of transforming power relations. The act of care-taking and the notion of power are in a complex relation. If somebody has the skills and knowledge to save your life it may give rise to a feeling of dependence.[^1]  In fact, the world's societies are absolutely dependent on medical workers, both for health and financial reasons. The pandemic has made this dependency obvious. It has emphasized the necessity of nurses and the fact that they are crucial in a time of crisis.  

There is an obvious conflict between nurses framed as crucial and powerful professionals and the more stereotypical image of nurses. Female nurses have long been subject to stereotyping and prejudice. They have, amongst other, been framed as less intelligent, angle-like or as sex symbols.[^2] Are the strong reactions in the sub-discourse on 4chan a consequence of a long-lived stereotype threatened by more powerful and heroic narratives? This important question cannot be answered before the portrayal of female nurses on 4chan has been properly analyzed. In that sense, this project is only the beginning of a much larger endeavor. It explores the attributes used to describe female nurses on 4chan and how such attributes contribute to a negative construction of female nurses as non-heroes. Hopefully the result of this analysis can be utilized in future projects that examine the relation between misogynist nurse hatred and transforming power relations. 

----

### **Research question**

This project explores attributes ascribed to female nurses in the sub-discourse on the online imageboard 4chan. More concretely, I look closer at how female nurses are negatively defined within this discourse as non-heroes. 

### **Research object**

All threads on /pol/ (4chan) listed under the subject "nurse", dating from the 25th of January to the 8th of June 2020. This amounts to 116 threads including more than 14 000 comments. The posts and comments are mainly written in English. 

### **Empirical source: 4plebs** 

The content on 4chan is ephemeral as the threads are automatically deleted from the website. To access posts dating back to January 2020 I have retrieved data from an archive of 4chan posts, [4plebs](https://archive.4plebs.org). This unofficial archive uses the software Asagi to save all the posts of selected 4chan boards. 4plebs uses Asagi to dump and archive all threads from selected 4chan boards, including /pol/. No threads are removed unless they are reported to contain child pornography, personally identifiable information, or copyrighted material not permitted under fair use doctrine.[^3]  

---

### **Method**

The data retrieved from 4plebs was prepared and analyzed in the attached Jupyter notebook (NurseHate.ipynb). The number and title of each step correspond with the number and title of each step in this notebook. 

#### 1. Preparing data

A script was written to scrape the text from all posts on the board /pol/ containing the word "nurse" (dating from 01.25.2020 to 06.08. 2020). This amounts to 116 posts with a total of more than 14 000 comments. I imported this scrape result and created a data frame (named "df") using pandas . Given the quantity of the dataset only the three first threads are displayed here. Moreover, the dataset contained unnecessary information. Redundant letters and post numbers were removed for the purpose of clarity. The column of comments to be analyzed is thus named "clean". 

#### **2. Preparing NLP**

Subsequently the data was to be prepared for the natural language processing (NLP). I imported spaCy, which is an open-source software library developed for advanced NLP. The textual data (4chan posts) was almost exclusively in English and I therefore loaded spaCy's core model for English (en_core_web_sm). Core models are models pretrained to predict i.a. part-of-speech tags and syntactic dependencies.  

##### Ten representative examples

I then printed the beginning of all 116 posts in order to search for representative examples. Interesting posts were printed in their totality, and my search resulted in ten representative posts. The ten posts display a certain variety within the 4chan discourse. In post72, for example, the author points out that "Nurses are necessary, without them hospitals don't function. They do 90% of the work inpatients need and are a wealth of knowledge. If you've every been hospitalized for any length of time you already know this."  Although some positive accounts of nurses are shared and discussed, the majority of the 4chan discussions revolve around the female nurse as incompetent, reckless, overworked, immature or as a female ignoring her motherly and feminine duties. The selection of example posts and comments underneath are therefore examples of mainly such degrading discussion. 

#### 3. Example analysis 

##### Thread A: Sexualized gene pools 

"Most women whom work at hospitals with any job are feminen, nuturing, of decent intellect (for a woman) and lonely (to the point of suicide). Their genes are valuable european genes that we should replicate. We need to snatch nurses for our european loyalist gene pool and turn them in to stay at home mums for >4 children so that they dont work for the state, we collapse the welfare state, produce white babies and prevent the nurse from dealing with negroes and other abominations STDs and can opener accidents. #pumpanurse"

This post is dense with attributions and assumptions. When the post is split into single sentences (sents) it becomes evident how it can be re-constructed as an argument: 

**P1**: "Most women whom work at hospitals with any job are feminen, nuturing, of decent intellect (for a woman) and lonely (to the point of suicide)."

**P2**: "Their genes are valuable european genes that we should replicate."

**C**: "We need to snatch nurses for our european loyalist gene pool and turn them in to stay at home mums for >4 children so that they dont work for the state, we collapse the welfare state, produce white babies and prevent the nurse from dealing with negroes and other abominations STDs and can opener accidents."

According to the author, turning nurses into "stay at home mums" is beneficial both for the European society and for the nurses themselves. The nurses are there to produce "white babies", to contribute to fulfilling the aim of white supremacy. Simultaneously, these nurses would be freed from their loneliness while realizing their feminine and nurturing nature. 

A further interesting aspect are the properties - nouns and adjectives - attributed to nurses. In a first step I took a closer look at the noun chunks in the post. Noun chunks are flat phrases, which have a noun as their head. Or, in other words, the noun chunk is the noun in addition to the words that describe it. The first noun chunk is "Most women". This generic appeals to some kind of female essence.[^4] The generic can be traced back to the sentence: "Most women whom work at hospitals with any job are feminen, nuturing, of decent intellect (for a woman) and lonely (to the point of suicide)". 

With this sentence the author establishes a category: women who work at hospitals. The quantifier "most" signifies that the majority of women who works at hospitals are assumed (by the author) to be feminine, nurturing, of decent intellect and lonely. These properties are attributed to women who work at hospitals in general, that is, if a woman would work at a hospital and not have these attributes she would be an exception from the rule. Furthermore, the property "decent intellect" is qualified by the phrase "for a woman", which means that the author assumes a female standard for intelligence that is lower than the male standard. Other relevant categories established are "home mums" and "nurses". Female nurses are also referred to as "they" which currently is in an antagonistic relation to a unspecified "we". The nurses, that is, employed women,  are also considered a consequence of the welfare state, which again is an institution that "we" must destroy. 

The most striking noun chunk, however, is the attribution "our european loyalist gene pool". Female nurses should not work because their bodies are needed for other aims. The white European female nurse is considered a pool of "valuable genes". 

###### Overview of comments in thread

The comments on the post described above are interesting as they display both resonance and disagreement. There are thirteen answers (comments) in the thread (see table), mainly expressions of agreement. Five representative posts are printed in their totality. coma3 refers to the assumed nature of female nurses (being a "hoe") as something unchangeable: "You cant turn a hoe into a housewife". The comments also add further and similar properties like overweight, sleep-deprived, lustful (coma5), rough and aggressive (coma6). coma6 furthermore refers to nurses as "bitches" and "sloots".[^5]  Additionally, coma5 reinforces the essentializing category "most women" whereas coma6 establishes the category "femininenurses". coma8 contributes to the sexualized narrative from the original post by stating that "I can get behind this classic fetish". 

In one sense coma7 constitutes an exception as the author points out that "They [female nurses] are all emotional wrecks from the suffering they see all day long over decades". Although the description "emotional wrecks" is clearly derogatory and diminishing, the comment appeals to actual events (exposure to suffering at hospitals) rather than a given female essence (e.g. emotional immaturity). 

##### Thread B: Incapability and attention 

"ok /pol/, let's talk nurses.  This is a collection of women and faggots who weren't smart enough to become doctors and now suddenly are national heroes. A piece of shit out of my asshole could become a nurse.  fucking retards"

The example post B is less complex than the post discussed above. The author addresses his audience (/pol/) directly and immediately sets the topic ("let's talk nurses"). Nurses are defined as "a collection of women and faggots who weren't smart enough to become doctors", a generic that displays both misogyny and homophobia. The topic of heroism is not discussed further, rather it is stated that anyone or anything could become a nurse, which implies that nurses as a category cannot qualify as national heroes. The post is ended with the provocative noun chunk "fucking retards". 

###### Overview of comments in thread

There are 308 responds to post B. A possible reason for the many reactions is the post's brief, direct, and provocative nature. Given the high number of of comments I used a random sample function to search for relevant examples. comb2 makes reference to the trend of nurses dancing on videos uploaded on [TikTok](tiktok.com), and labels nurses as "dancing cunts".  On the other hand, comb4 makes use of information the author allegedly has from the inside; his/her sister who is a nurse. Nurses are referred to as "scum of the earth", bullies and as sexually interested in the older male doctors. The angel-like figure of the goodhearted and kind nurse is represented to some extent, but it is claimed that such nurses are bullied out of the profession. The ones remaining are those who seek glory and who will use their bodies to get it (comb7). It is even claimed that some women only became nurses to get status, attention, and money (comb10). 

##### Thread C: Necessity and heroism 

"Every other thread is some anti-nurse topic. Nurses are necessary, without them hospitals don't function. They do 90% of the work inpatients need and are a wealth of knowledge. If you've every been hospitalized for any length of time you already know this. Is it because /pol/ is filled with real life incels or is it Chinese shills?"

The author begins by addressing the fact that the pandemic has led to a high number of threads dominated by a negative attitude towards nurses. Then the necessity of nurses in terms of work capacity and knowledge is emphasized. Further, the post appeals to personal experience; people who have been hospitalized should be aware of this necessity. The post ends with a question. Are the negative attitudes displayed a result of the discussants in 4chan being "real life incels" or is it due to "Chinese shills"? The term incel is short for "involuntary celibates" and refers to an online subculture of men who consider themselves unable to establish romantic and sexual relationships despite a desire to do so. Importantly, incels blame women for their involuntary celibate.[^6]  This is a clear reference to the misogynist dimension of the nurse hatred displayed on 4chan.  

A "shill" is someone who helps a person persuade other people to do something. There has been online discussion on the so-called "Chinese shills", where it is suggested that the Chinese government pays both American and Chinese citizens to spread pro-Chinese and anti-American propaganda.[^7] The term gained a new meaning with the Covid-19 outbreak. There are online communities committed to reveal various government's alleged attempts to obscure and downplay the amount of cases in China and on a global scale.[^8] The post seems to imply that the skepticism and hatred towards nurses may be fueled by forces trying to diminish the current global health crisis.   

###### Overview of comments in thread 

The post described above is atypically positive and primarily constructive, which is reflected in more diverse comments. One of the first comments (comc2) states the following: "Nurses and doctors are unironically heroes right now. Risking their lives to save other lives. Definition of heroism." A further comments admits to the importance of nurses but underlines that nurses are essentially motivated by the need for attention (comc5), whereas yet another post claims "Nurses should die in a fire" due to their selfishness and uselessness (comc34). 

The last comment printed in its totality in the notebook reads as follows: "There’s constant demand for nurses because our societies are constantly growing fatter, older, and sicker. Once the boomers die off and wine aunts an hero demand for nurses will collapse" (comc35). In this comment the celebration of nurses is explained exclusively by a society in decay; obesity, age, and increasing health issues. The term "boomers" is inspired by the term baby boomers and refers to middle aged or older people who do not understand the mindset of the so-called "generation Z".[^9] The wine aunts may refer to the generation before, feeding into the narrative of a sick society, here on the example of elders and alcoholism. The prophecy seems to be that once these people are dead society will revitalize and the demand for nurses will decrease, which will lead to collapse of the heroic nurse figure. In other words, the celebration of nurses as heroes is considered a consequence of a sick society.  

----

### **Preliminary results**

The project was aimed at exploring attributes used to describe female nurses on 4chan and how such attributes contribute to a negative construction of female nurses as non-heroes. I used data from a 4chan archive to create a data frame with 116 threads including more than 14 000 comments revolving around the topic of nurses. Although the project was informed by many of the 116 threads, the thorough analysis focused on three main examples (named thread A, B, and C). The analysis of the first example thread was concerned with the nurse reduced to a female body, the second with the incapability and desire of attention attributed to nurses, and the third with the 4chan community's discussion of the necessity of nurses. While each of the three threads were written from a specific angle they all display numerous attempts of diminishing the female nurse, mostly as a way of disclaiming her status as a hero. 

The most striking aspect of thread A are the two pairs of contrast used to reduce female nurses into female bodies. The post claims nurses to be lonely to the point of suicide and thereby not fulfilling their natural given task as gene pools for reproduction. They are examples of working women who neglect their role as nurturing mothers in a white supremacist society. These women are thus not only considered a tool for survival of the human species; their bodies are politicized as means towards a racist end. In the comments a further pair of contrast is introduced. Nurses are described as "bitches" and "sloots", and are contrasted by the figure of the housewife. 

Thread B is more concerned with disqualifying the concept of heroism. Nurses are claimed to be women who are too unintelligent to become doctors, and the conclusion from this assumption seems to be that anything (and anyone) could become a national hero. The term "National hero" is in other words a worthless attribution. The analyzed comments on the post expand on this narrative by framing nurses as bullies desiring glory and sex. The thread thus displays an interesting contradiction between the unintelligent nurse and the manipulative nurse exploiting her position for attention and sexual relations. 

The post in thread C is an exception to the many anti-nurse posts in the data frame. Nurses are portrayed as necessary, knowledgeable, and hard-working. Some comments undermine the post and it is claimed that nurses are "unironically heroes". The need for an adjective like "unironically" is a consequence of the ironically distorted meaning of the term "hero" in the /pol/ discourse on nurses. While some comments argue that nurses are important yet attention-seeking, others insist that nurses are selfish and unintelligent "attention whores". In one sense the circle is ended the comment that claims that nurses are indeed heroes, but only because we live in a sick society that is dependent nurses. Once society rises again, it is claimed, "the hero demand for nurses will collapse". 

To summarize, an important shared point of criticism in the nurse hate threads is that female nurses are occupying a space that was not intended for them. As they are working women and not stay-home mums they are lonely and depressed, unable to realize their true feminine and nurturing essence. They are too unintelligent to become doctors, yet they find their way to the hospital through a career as a nurse. In other words, they wrongfully enter a male domain. Similarly, the term "(national) heroes" is wrongfully assigned to this group by the mainstream. In combination with the profession (female) nurse the term becomes worthless. Heroism is not intended for (such) women. To return to the question posed in the beginning: there is an obvious correlation between nurse hatred and misogyny in the 4chan sub-discourse. Furthermore, many of the posts and comments express direct or implicit anger and anxiousness about transforming gendered power relations. De-constructing and understanding the nature of the hatred against nurses thus requires an analysis of the misogyny grounding this discourse, both in a general and in a pandemic-related sense.  

____

### **Challenges and concerns** 

###### Data

The first substantial challenge was to access the relevant data. Given the high number of posts published every day, the threads on /pol/ are constantly deleted. You can normally only access threads from the same day as all earlier threads are already deleted. As the beginning and development of the Covid-19 influenced  discourse on nurses were objects of interest, I needed access to posts dating back to January 2020. Thus, the project was dependent on an archive (4plebs, see "Empirical source").  For future 4chan related projects the software used by 4plebs (Asagi imageboard dumper) could be a way of creating an independent archive for 4chan threads.  

###### Analysis 

A data frame with 116 posts and more than 14 000 comments constitutes a well of opportunities. For time and capacity reasons this project looked closer at three relevant posts (including their comments) to cover a certain variety in both argumentation and perspective. Further examples, however, would surely have broadened the range of displayed attitudes towards nurses. Nevertheless, the project developed a methodology for exploring the online nurse-hatred, both in terms of codes and analysis strategies. This methodology can be utilized further to analyze a larger number of threads.   

---

[^1]: Although many nurses report not feeling powerful in their profession and consider caring and power to be opposite concepts. See Rafael (1996). 
[^2]: See for example Kalisch et al. (1982), Hallam (2005) and Daly et al. (2005). 
[^3]: Further information: "How do I get things removed?" in FAQ 4plebs: https://archive.4plebs.org/_/articles/faq/. 
[^4]: "Sloot" is a transformation of the derogatory term "slut" where the number of o's may indicate the degree of "sluttyness". 
[^5]: For an illuminating debate on generics of social kinds and (hidden) essence see Haslanger (2012) and Leslie (2007). 
[^6]: See for example Byerly (2020). 
[^7]: See for example Kredo (2012). 
[^8]: An example is the group "nCovshills" on reddit.com: https://www.reddit.com/r/ncovshills/.
[^9]: See Lorenz (2019). 

### **Literature**

Byerly, Carolyn, 2020, "Incels online reframing sexual violence". *The Communication Review*,  DOI: 10.1080/10714421.2020.1829305. 

Daly, John et al., 2005, *Professional Nursing: Concepts, Issues, and Challenges*. 

Hallam, Julia, 2005, "Angels, Battleaxes and Good - Time Girls: Cinema's images of nurses". G. Harper, & A. Moore (Eds.), *Signs of Life: Medicine and Cinema*, 105-120. 

Haslanger, Sally, 2012, "Ideology, Generics, and Common Ground". Haslanger, *Resisting Reality: Social Construction and Social Critique*, 446-482. 

Kalisch et al. 1982, "The Nurse as a Sex Object in Motion Pictures, 1930 to 1980". *Research in Nursing and Health*, 147-154. 

Kredo, Adam, 2012, "The China Shills". *The Washington Free Bacon*, 18.07.2012, https://freebeacon.com/politics/the-china-shills/. 

Leslie, Sarah-Jane, 2007, “Generics and the Structures of the Mind”. *Philosophical Perspectives, 21, Philosophy of Mind*, 375–403. 

Lorenz, Taylor, 2019, "‘OK Boomer’ Marks the End of Friendly Generational Relations". *The New York Times*, 29.10.2019, https://www.nytimes.com/2019/10/29/style/ok-boomer.html. 

Rafael, Adeline R.F., 1996, "Power and caring: A dialectic in nursing". *Advances in Nursing Science*, 19(1). 3–17.

*All links retrieved 15.12.2020.*

  

