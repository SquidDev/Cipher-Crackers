from __future__ import print_function
from math import log10, log

from sys import argv

input_file, name, ngram_length = argv[1:]
ngram_length = int(ngram_length)
contents = ""
with open(input_file, "r") as x:
	contents = x.read()

def id(x):
	i, s = 1, 0
	for c in x[::-1]:
		s += (ord(c) - 65) * i
		i *= 26
	return s

res = [0] * 26**ngram_length
i, sum = 0, 0
for line in contents.strip().split("\n"):
	split = line.split(" ")
	count = int(split[1])
	res[id(split[0])] = count

	sum += count
	i += 1

sum = float(sum)
floor = log10(0.01 / sum)
for i in xrange(len(res)):
	count = res[i]
	if count == 0:
		res[i] = floor
	else:
		res[i] = log10(count / sum)


with open(name + ".h", "w") as x:
	x.write("#pragma once\n")
	x.write("\tconst float" + name + "[] = {\n")
	for item in res:
		x.write("\t\t" + str(item) + ",\n")
	x.write("\t};\n")
