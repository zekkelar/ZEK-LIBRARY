import datetime
import mysql.connector

import database.conn as conn


class this_bookmark_internal:
	def __init__(self):
		self.now = datetime.datetime.now()
		self.getter = conn.connect()

	def add_bookmark(self, name, username):
		get = self.getter.connect()
		query = f"INSERT INTO bookmark_product (identifier, username, datetime) VALUES (%s, %s, %s)"
		val = (name, username, self.now)
		try:
			get[1].execute(query, val)
			get[0].commit()
			get[0].close()
			return True
		except Exception as e:
			print(e)
			return False

	def isBookmark(self, name, username):
		get = self.getter.connect()
		query = f"SELECT * FROM bookmark_product WHERE identifier='{name}' AND username='{username}'"
		get[1].execute(query)
		catch = get[1].fetchone()
		if catch != None:
			return True
		else:
			return False

	def remove_bookmark(self, name, username):
		get = self.getter.connect()
		query = f"DELETE FROM bookmark_product WHERE identifier='{name}' AND username='{username}'"
		try:
			get[1].execute(query)
			get[0].commit()
			get[0].close()
			return True
		except Exception as e:
			print(e)
			return False

	def get_all_bookmark(self, username):
		get = self.getter.connect()
		query = f"SELECT * FROM bookmark_product WHERE username='{username}'"
		get[1].execute(query)
		catch = get[1].fetchall()
		if catch != None:
			return catch
		else:
			return False

	def remove_all(self, username):
		get = self.getter.connect()
		query = f"DELETE FROM bookmark_product WHERE username='{username}'"
		try:
			get[1].execute(query)
			get[0].commit()
			get[0].close()
			return True
		except Exception as e:
			print(e)
			return False

