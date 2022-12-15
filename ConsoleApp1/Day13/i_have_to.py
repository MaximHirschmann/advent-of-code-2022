from functools import cmp_to_key

def cmp(list1, list2):
	if type(list1) is int and type(list2) is int:
		return (list1 > list2) - (list1 < list2)
	if type(list1) is int:
		return cmp([list1], list2)
	if type(list2) is int:
		return cmp(list1, [list2])
	
	for x, y in zip(list1, list2):
		if temp := cmp(x, y):
			return temp

	return (len(list1) > len(list2)) - (len(list1) < len(list2))


if __name__ == "__main__":
	with open("input.txt", "r") as f:
		lines = f.readlines()

	res1 = 0
	inputs = [[[2]], [[6]]]
	for i in range(0, len(lines), 3):
		list1 = eval(lines[i])
		list2 = eval(lines[i+1])
		inputs.append(list1)
		inputs.append(list2)

		if cmp(list1, list2) == -1:
			res1 += i // 3 + 1

	print(res1)

	inputs.sort(key = cmp_to_key(cmp))
	
	p1 = inputs.index([[2]]) + 1
	p2 = inputs.index([[6]]) + 1
	print(p1 * p2)