notebooks := $(wildcard ./**/*.ipynb)
md_pages := $(patsubst notebooks/%.ipynb,docs/%.md,$(notebooks))

build.env: ; conda env create -f environment.yml
build.site: $(md_pages)

docs/%.md: notebooks/%.ipynb
	jupyter nbconvert\
		--to markdown $<\
		--output-dir $(dir $@)\
		--template=src/to_markdown.tpl
