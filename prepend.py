import glob

p = """
//============================================================
 // Student Number : S10219526, S10227463
 // Student Name : Qin Guan, Richard Paul Pamintuan
 // Module Group : T07
//============================================================


"""

for filename in glob.iglob("./" + '**/**.cs', recursive=True):
    with open(filename, 'r') as original: data = original.read()
    with open(filename, 'w') as modified: modified.write(p + data)
