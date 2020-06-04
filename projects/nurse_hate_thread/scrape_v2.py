import requests
from bs4 import BeautifulSoup

url = 'https://archive.4plebs.org/pol/search/subject/nurse/start/2020-01-25'

all_posts = [] # Alle poster
all_links = [] # Alle linker til tråder fra søkeresultat-siden

# Finner linker til poster som skal scrapes
def findLinks(url):
    html = requests.get(url).text # henter all html-kode fra link
    content = BeautifulSoup(html, 'html.parser') # "sorterer html"
    links = content.find_all('a', {'class': 'btnr parent'})
    

    for link in links:
        if link.text == 'View':
            if link['href'] not in all_links: # ingen duplicates
                all_links.append(link['href'])
                print("legger til " + link['href'])

# lager en funksjon til scrapingen 
def scrape(url):
    print("scraper " + url)
    html = requests.get(url).text # henter all html-kode fra link
    content = BeautifulSoup(html, 'html.parser') # "sorterer html"

    comments = []
    # Identifiserer alle post-titlene
    titles = content.find_all('h2', {'class': 'post_title'})
    
    # Identifiserer alle kommentarene
    post_text = content.find_all('div', {'class': 'text'})
    
    # for hvert post-innhold: legg til i all_text
    for content in post_text:
        comments.append(content.text)
    
    all_posts.append({"title": titles[0].text, "comments": comments})

for i in range(1, 5):
    findLinks(url+'/page/'+str(i)) 

for link in all_links:
    scrape(link)

#print(all_posts)



# Lager en tekstfil vi kaller "scrape_resultat_v2"

x = open("scrape_resultat_v2.txt", "w", encoding="utf-8")

x.write(str(all_posts))

print("ferdig!")
