import datetime
import mysql.connector

import database.conn as conn


class this_books:
	def __init__(self):
		self.now = datetime.datetime.now()
		self.getter = conn.connect()

	def upload_books(self, img_name, author,
						username, title, years, deskripsi,
							category, pdf_name):
		get = self.getter.connect()
		query = f"""
				INSERT INTO books_ (image, author, username, title, years,
							 deskripsi, category, datetime, pdf, item_id, views)
				VALUES (%s,%s,%s,%s, %s,%s,%s, %s, %s, %s, %s)
				 """
		val = (img_name, author, username, title, years, deskripsi, category, self.now, pdf_name, '0', '0')
		try:
			get[1].execute(query, val)
			get[0].commit()
			get[0].close()
			return True
		except Exception as e:
			print(e)
			return False

	def get_views(self, name):
		get = self.getter.connect()
		query = f"SELECT * FROM books WHERE image='{name}'"
		get[1].execute(query)
		viw = get[1].fetchone()
		return viw[-1]

	def update_views(self, name, views):
		get = self.getter.connect()
		query = f"UPDATE books_ SET views='{views}' WHERE image='{name}'"
		try:
			get[1].execute(query)
			get[0].commit()
			get[0].close()
			return True
		except Exception as e:
			print(e)
			return False

	def read_books(self):
		get = self.getter.connect()
		query = "SELECT * FROM books_"
		get[1].execute(query)
		return get[1].fetchall()

	def read_by_name(self, name):
		get = self.getter.connect()
		query = f"SELECT * FROM books_ WHERE image='{name}'"
		get[1].execute(query)
		return get[1].fetchone()

	def get_total_books_upload_byusername(self, username):
		get = self.getter.connect()
		query = f"SELECT * FROM books_ WHERE username='{username}'"
		get[1].execute(query)
		info = get[1].fetchall()
		if info != None:
			return str(len(info))
		else:
			return 'No books'

	def get_book_uploaded_byusername(self, username):
		get = self.getter.connect()
		query=f"SELECT * FROM books_ WHERE username='{username}'"
		get[1].execute(query)
		res = get[1].fetchall()
		if res != None:
			return res
		else:
			return False