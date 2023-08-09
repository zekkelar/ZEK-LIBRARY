from fuzzywuzzy import fuzz
import datetime
import mysql.connector


import database.conn as conn 


class this_search_fuzz:
	def __init__(self):
		self.now = datetime.datetime.now()
		self.getter = conn.connect()

	def read_books(self):
		get = self.getter.connect()
		query = "SELECT * FROM books_"
		get[1].execute(query)
		return get[1].fetchall()

	def find_books(self, identifier):
		get = self.getter.connect()
		query = f"SELECT * FROM books_ WHERE image='{identifier}'"
		get[1].execute(query)
		return get[1].fetchone()

	def fsm(self, query):
		best_match = []
		book = self.read_books()
		for res in book:
			similarity = fuzz.partial_ratio(query, res[3])
			print(similarity)
			if ((similarity > 10) or (query.lower() in res[3].lower()) or (query.lower() in res[5].lower())):
				best_match.append(res[0])
		if len(best_match) == 0:
			return False
		else:
			return(self.cari_buku(best_match))

	def cari_buku(self, list_match):
		listB = []
		for x in list_match:
			find_ = self.find_books(x)
			listB.append(find_)

		return listB


if __name__ == "__main__":
	run = books()
	run.fsm()