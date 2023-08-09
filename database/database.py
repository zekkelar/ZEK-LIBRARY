import datetime
import mysql.connector

import database.conn as conn


class angkatan:
	def __init__(self):
		self.getter = conn.connect()

	def get_angkatan(self):
		get = self.getter.connect()
		query = "SELECT * FROM angkatan"
		return get[1].fetchall(query)


