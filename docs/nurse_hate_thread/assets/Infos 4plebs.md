Some infos about archive.4plebs.org (https://archive.4plebs.org/_/articles/faq/)

*Copied 22.05.2020*

---

# Frequently asked questions

Last updated on Nov 30 2018.

### What is "archive.4plebs.org"?

4plebs is an an unofficial archive of certain boards of [4chan.org](https://www.4chan.org/). Our archives include boards /adv/, /f/, /hr/, /o/, /pol/, /s4s/, /sp/, /tg/, /trv/, /tv/ and /x/. This archive is not part of [4chan](https://www.4chan.org/) and [4chan](https://www.4chan.org/) rules do not apply here.



#### Why do you archive [4chan](https://www.4chan.org/)?

Threads on [4chan.org](https://www.4chan.org/) are alive for a relatively short time and get removed after awhile. This site archives all threads on certain boards to preserve them for later viewing.



#### How do you archive [4chan](https://www.4chan.org/)?

We use a fetcher software called [Asagi](http://eksopl.github.io/asagi/) to download threads and images in real time. We use software called [FoolFuuka](http://www.foolz.us/info/foolfuuka) for delivering this web front end. Advanced features like search and statistics are also provided with [FoolFuuka](http://www.foolz.us/info/foolfuuka).



### How do I contact you/administrator of this site?

Email: [admin@4plebs.org](mailto:admin@4plebs.org)

Twitter: [@4plebs](https://twitter.com/4plebs)

Discussion Board: [>>>/plebs/](http://archive.4plebs.org/plebs/)



### How do I get things removed?



#### Reports

This site has a report feature that lets you request removal of any thread, post or image. Report button is included in all post. Please remember to give a valid reason for removal of content.

Valid reasons for post/image/thread removal:

- Child pornography (See [Dost test criteria](https://en.wikipedia.org/wiki/Dost_test#Criteria))
- Personally identifiable information (See [NIST definition](https://en.wikipedia.org/wiki/Personally_identifiable_information#NIST_definition))
- Copyrighted material not permitted under fair use doctrine (See [fair use factors](https://en.wikipedia.org/wiki/Fair_use#U.S._fair_use_factors))

Reports may be removed for any reason. We make no promises that your report will be acted upon. Images can be removed and then banned from reappearing on the archive. Other removal request should be submitted via email [admin@4plebs.org](mailto:admin@4plebs.org).



#### Will you remove libel?

Email us and we can talk.



### When was this archive started?

4plebs started archiving with current software on early April of 2013.

| Board                 | Start date       |
| :-------------------- | :--------------- |
| /hr/, /tg/, /tv/, /x/ | April 4 2013     |
| /s4s/                 | November 19 2013 |
| /pol/                 | November 29 2013 |
| /o/                   | January 8 2014   |
| /adv/                 | January 17 2014  |
| /trv/                 | March 14 2014    |
| /f/                   | March 16 2014    |
| /sp/                  | March 1 2015     |





### How long do you store full images for?

~~Currently around 12 months.~~ As long as possible.
Removed files will be uploaded to archive.org from now on.
List of data dumps [here](https://archive.4plebs.org/_/articles/credits/#4plebsdumps).



### What is ghost posting?

It's a way of continuing discussion after the thread in question has been removed from 4chan. Ghost posts will only appear on 4plebs and they will only bump the thread up in the ghost index. You can't post images with ghost posts.



### What was the deal with not4plebs?

not4plebs.org was a containment archive for /sp/. It was started on 1. March 2015 and it was merged to this archive in 1. September 2015.



### Do you have an Application Programming Interface (API) and what are it's limitations?

Yes we have several JSON API endpoints. Quick examples:

Index

```
http://archive.4plebs.org/_/api/chan/index/?board=adv&page=1
```

Post

```
http://archive.4plebs.org/_/api/chan/post/?board=adv&num=17527202
```

Thread

```
http://archive.4plebs.org/_/api/chan/thread/?board=adv&num=16627902
```

Search

```
http://archive.4plebs.org/_/api/chan/search/?boards=adv.trv&text=test&page=1
```

Search API is limited to 5 requests per minute. Other end points aren't limited.

[Documentation from original developers](http://foolfuuka.readthedocs.io/en/latest/code_guide/documentation/api.html) (somewhat incorrect). [Accurate documentation](https://4plebs.tech/foolfuuka/)

Error object looks like this

```
{"error":"Search limit exceeded. Please wait 5 seconds before attempting again."}
```



### Can you give me legal information about the site?

See [this page for legal information](https://archive.4plebs.org/_/articles/legal/).



### Are there any more archive sites like this?

Yes. List of other Fuuka/FoolFuuka archivers [here](https://archive.4plebs.org/_/articles/credits/#archives).



### Images from i.4pcdn.org are blocked on my end. Can I use old image links?

Yes. [Click here to toggle classic image links](https://archive.4plebs.org/_/classic_uri/).

### What's the status on importing old 2010-2013 posts?

Slow but steady. Many thumbnails and images were lost by previous archives. Here's the current progress:

| Board | Posts | Thumbnails      | Full images   |
| :---- | :---- | :-------------- | :------------ |
| [s4s] | 100%  | Not yet started | Not available |
| /tv/  | 100%  | 100%            | Not available |
| /tg/  | 100%  | 100%            | 100%          |



### What's the status on importing old 2006-2009 posts?

Not started yet.

| Board | Posts           |
| :---- | :-------------- |
| /hr/  | Not yet started |
| /o/   | Not yet started |
| /sp/  | Not yet started |
| /tg/  | Not yet started |
| /trv/ | Not yet started |
| /tv/  | Not yet started |
| /x/   | Not yet started |