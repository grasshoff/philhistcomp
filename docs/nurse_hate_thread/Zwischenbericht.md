[Work in progress]

# **Nurse hate threads - "She's not a hero"**

### Background

Since the beginning of the covid-19 pandemic nurses (alongside doctors and other medical workers) have been celebrated globally for risking their health and working overtime in order to fight the virus. The work done by nurses has been subject to increased attention, which has led to a debate on their work conditions, wages, and the social recognition of their profession. 

The mainstream celebration of nurses has, however, also provoked hatred and frustration in sub-discourses. On the male dominated online imageboard [4chan](4chan.org) there has been a rise of discussion threads revolving around hatred against nurses. These threads mostly appear on the board /pol/ ("Politically Incorrect"). Such threads are often referred to as "nurse hate threads". The vast majority of these discussions combine the hatred against nurses with explicit misogyny; the nurses are mostly incapable, immoral or immature in virtue of being women. Importantly, these characterizations of nurses mostly aim at creating an antithesis to the figure of the hero. Nurses (predominantly in virtue of being women) are ridiculed as not skilled, moral, mature, or smart enough to be real heroes. Rather, they are assumed to exploit the current attention in order to stage themselves as heroes. Thus, they are not only framed as not having the properties of a hero. They are framed as the explicit opposite of a hero; a non-hero. 

### **Project motivation**

The observations sketched above give rise to multiple questions. Why is the figure of a female hero so negatively received in certain online sub-discourses? Are such misogynist attitudes augmented in the context of a pandemic? The covid-19 virus is a source of global fear and instability. Thus, there is reason to believe that the hatred against female nurses is partly nurtured by a notion of powerlessness and a fear of transforming power relations. There is indeed a connection between the caring provided by nurses and the notion of power. If somebody has the skills and knowledge to improve or save your life it may give rise to a strong feeling of dependence.[^1] In the current situation the world's societies are absolutely dependent on medical workers, both for health and financial reasons. In other words, the pandemic has emphasized the necessity of nurses and the fact that their profession is crucial in a time of crisis.  

There is an obvious conflict between nurses framed as crucial, courageous and powerful professionals and the more stereotypical image of nurses. Female nurses have always been subject to stereotyping and prejudice, especially in cinema and pornography. They have, amongst other, been framed as less intelligent, angle-like or as sex symbols.[^2] Are the strong reactions in the sub-discourse on 4chan a consequence of a long-lived stereotype threatened by more powerful and heroic narratives? This important question cannot be answered before the portrayal of female nurses on 4chan has been properly analyzed. In that sense, this project is only the beginning of a much larger endeavor. The project aims at exploring which attributes are used to describe female nurses on 4chan and how such attributes contribute to a negative construction of female nurses as non-heroes. Additionally, it seeks to identified, how female nurses are classified in different ways. Hopefully the result of this analysis can be utilized in future projects that examine the relation between misogynist nurse hatred and transforming power relations. 

----

### **Research question**

This project investigates how female nurses are described in online debate on the board /pol/ on 4chan. More concretely, I look closer at how nurses are negatively defined within this discourse as non-heroes. 

### **Research object**

All threads on /pol/ (4chan) containing the word "nurse", dating from the 25th of January to 8th of June 2020. This amounts to 116 threads including xxxxx comments. The posts and comments are mainly written in English. 

### **Empirical source: 4plebs** 

The threads on 4chan are ephemeral as they are constantly deleted from the website. To access posts dating back to January 2020 I have retrieved data from an archive of 4chan posts, [4plebs](archive.4plebs.org). This unofficial archive uses the software [Asagi](http://eksopl.github.io/asagi/) to save all the posts of selected 4chan boards. 4plebs uses Asagi to dump and archive all threads from selected 4chan boards, including /pol/. No threads are removed unless they are reported to contain child pornography, personally identifiable information, or copyrighted material not permitted under fair use doctrine.[^3]  

---

### **Method**

The data retrieved from 4plebs was prepared and analyzed in the attached Jupyter notebook (NewNurseHate.ipynb **[current name]**). The number and title of each step correspond with the number and title of each step in this notebook. 

#### 1. Preparing data

A script was written to scrape the text from all posts on the board /pol/ containing the word "nurse" (dating from 01.25.2020 to 06.08. 2020). This amounts to 116 posts with a total of more than xxxxxx comments. I imported this scrape result and created a data frame (named "df") using pandas . Given the quantity of the dataset I have chosen to only show the three first threads in the notebook. Moreover, the dataset contained unnecessary information and needed to be cleaned. Redundant letters and post numbers were removed for the purpose of clarity. The column of comments to be analyzed is thus named "clean". 

#### **2. Preparing NLP**

Subsequently the data was to be prepared for the natural language processing (NLP). I imported spaCy, which is an open-source software library developed for advanced NLP. The textual data (4chan posts) was almost exclusively in English and I therefore loaded spaCy's core model for English (en_core_web_sm). Core models are models pretrained to predict i.a. part-of-speech tags and syntactic dependencies. These two features were especially important in detecting both attributions ascribed to nurses and the various ways of classifying nurses in groups. 

##### **Ten representative examples**

I then printed the beginning of all 116 posts in order to search for representative examples. Interesting posts were printed in their totality, and my search resulted in ten representative posts. The ten posts display a certain variety within the 4chan discourse. In exem8, for example, the author points out that "Nurses are necessary, without them hospitals don't function. They do 90% of the work inpatients need and are a wealth of knowledge. If you've every been hospitalized for any length of time you already know this."  Although some positive accounts of nurses are shared and discussed, the majority of the 4chan discussions revolve around the female nurse as incompetent, reckless, overworked, immature or as a female ignoring her motherly and feminine duties. The selection of example posts underneath are therefore examples of mainly such degrading discussion. 

#### 3. Example analysis 

##### Example A: 

"Most women whom work at hospitals with any job are feminen, nuturing, of decent intellect (for a woman) and lonely (to the point of suicide). Their genes are valuable european genes that we should replicate. We need to snatch nurses for our european loyalist gene pool and turn them in to stay at home mums for >4 children so that they dont work for the state, we collapse the welfare state, produce white babies and prevent the nurse from dealing with negroes and other abominations STDs and can opener accidents. #pumpanurse"

This post is dense with attributions and assumptions. As can already be observed through the splitting of the post into single sentences (sents) it can be re-constructed as an argument: 

**P1**: Most women whom work at hospitals with any job are feminen, nuturing, of decent intellect (for a woman) and lonely (to the point of suicide)

**P2**: Their genes are valuable european genes that we should replicate.

**C**: We need to snatch nurses for our european loyalist gene pool and turn them in to stay at home mums for >4 children so that they dont work for the state, we collapse the welfare state, produce white babies and prevent the nurse from dealing with negroes and other abominations STDs and can opener accidents.

According to the author, turning nurses into "stay at home mums" is beneficial both for the European society and for the nurses themselves. The European society would use the nurses to produce "white babies", that is, a white supremacist aim. Simultaneously, these nurses would be freed from their loneliness while realizing their feminine and nurturing nature. 



##### Example B:

"ok /pol/, let's talk nurses.  This is a collection of women and faggots who weren't smart enough to become doctors and now suddenly are national heroes. A piece of shit out of my asshole could become a nurse.  fucking retards"

##### Example C:

"AAHAHAHAHHAHAHA  Let's be clear, all nurses as a class deserve severe punishment for being shameless tik tok dancers, but this is a good start. https://www.dailydot.com/irl/tiktok-jeanette-mckenna-death/ >Jeanette McKenna, 53, passed away from what her co-workers suspect to be the coronavirus. She was recently seen dancing with a 102-year-old patient on TikTok.  If you are a nurse you are a piece of shit."

##### Example D:

"Was talking with my sister who is a career nurse. Very good at her job and dedicated to her craft.  We were talking about the nurse tiktok videos and how they negatively affect public perceptions of this whole situation and she should probably advise her staff to stop it or keep it private. I told her some people (meaning females) are too emotionally immature to handle praise and hero worship. She then had this to say.  Pic related"

----

### **Preliminary results**

### **Challenges and concerns** 

- Archive, 4plebs
- Data quantity 

----

### **Footnotes**

[^1] Although many nurses themselves report not feeling any power in their profession, and that they consider caring and power to be opposite concepts. See Rafael (1996). 

[^2] See for example Kalisch et al. (1982), Hallam (2005) and Daly et al. (2005). 

[^3] More infos: "How do I get things removed?" in FAQ 4plebs: https://archive.4plebs.org/_/articles/faq/, downloaded 16.09.2020. 

---

### **Sources**

Daly, John et al., 2005, *Professional Nursing: Concepts, Issues, and Challenges*, New York: Springer Publishing. 

Hallam, Julia, 2005, "Angels, Battleaxes and Good -Time Girls: Cinema's images of nurses". In G. Harper, & A. Moore (Eds.), *Signs of Life: Medicine and Cinema*, London, New York: Wallflower Press, 105-120. 

Kalisch et al. 1982, "The Nurse as a Sex Object in Motion Pictures, 1930 to 1980", in *Research in Nursing and Health*, New York: Wiley, 147-154. 

Rafael, Adeline R.F., 1996, "Power and caring: A dialectic in nursing". In *Advances in Nursing Science*, 19(1), 3–17.

  

