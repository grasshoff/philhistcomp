all: latx

watch:
		watchmedo shell-command --patterns="*.md" --command=make

src = $(wildcard *.md)

OST = temp
PANDOC_META = author.yaml
PANDOC_BIB = --biblatex --filter pandoc-citeproc
PANDOC_BIB += --bibliography=CoronaVirus.bib
PANDOC_BIB += --csl=chicago-note-bibliography-with-ibid.csl
PANDOC_OPT = -s --template=eisvogel.tex

PANDOC_MKD = -f markdown_github+yaml_metadata_block+implicit_figures+table_captions

latx: $(src)
	pandoc $(PANDOC_META) $(PANDOC_BIB) $(PANDOC_OPT) $(src) -o $(basename $(src)).tex
	latexmk -f -pdf -outdir=$(OST) $(basename $(src)).tex
	cp $(OST)/$(basename $(src)).pdf .

paper.pdf: $(src)
	pandoc -s author.yaml $(src) --filter pandoc-citeproc -o book.pdf --from markdown --template ./eisvogel.tex -N

clean:
	rm -rf tex2pdf*
