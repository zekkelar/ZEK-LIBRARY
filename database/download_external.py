
import datetime
import mysql.connector

import database.conn as conn


class this_download_journal_external:
	def __init__(self):
		self.now = datetime.datetime.now()
		self.getter = conn.connect()

	def add_download_external(self, added_date, title, author, description, publication_date, language, source, link_download, img_url, username):
		get = self.getter.connect()
		query = f"""INSERT INTO download_journal_external (added_date, title, author, description, publication_date, language, source, link_download, img_url, username)
					VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s)"""
		val = (added_date, title, author, description, publication_date, language, source, link_download, img_url, username)
		try:
			get[1].execute(query, val)
			get[0].commit()
			get[0].close()
			return True
		except Exception as e:
			return False

	def isDownload(self, username, source):
		get = self.getter.connect()
		query = f"SELECT * FROM download_journal_external WHERE username='{username}' AND title='{source}'"
		get[1].execute(query)
		catch = get[1].fetchone()
		if catch != None:
			return True
		else:
			return False

	def remove_download(self, username, source):
		get = self.getter.connect()
		query = f"DELETE FROM download_journal_external WHERE username='{username}' AND title='{source}'"
		try:
			get[1].execute(query)
			get[0].commit()
			get[0].close()
			return True
		except Exception as e:
			print(e)
			return False

	def get_all_download(self, username):
		get = self.getter.connect()
		query = f"SELECT * FROM download_journal_external WHERE username='{username}'"
		get[1].execute(query)
		catch = get[1].fetchall()
		if catch != None:
			return catch
		else:
			return False

	def remove_all(self, username):
		get = self.getter.connect()
		query = f"DELETE FROM download_journal_external WHERE username='{username}'"
		try:
			get[1].execute(query)
			get[0].commit()
			get[0].close()
			return True
		except Exception as e:
			print(e)
			return False