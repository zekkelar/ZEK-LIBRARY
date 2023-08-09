import datetime
import mysql.connector


import database.books as books
import database.conn as conn

class this_account:
	def __init__(self):
		self.now = datetime.datetime.now()
		self.getter = conn.connect()
		self.books = books.this_books()

	def login(self, username, password):
		get = self.getter.connect()
		query =f"SELECT * FROM account WHERE username='{username}' AND password='{password}'"
		get[1].execute(query)
		try:
			if get[1].fetchone():
				return True
			else:
				return False
		except Exception as e:
			return False

	def register(self, username, password, machine_id):
		get = self.getter.connect()
		find_user = f"SELECT * FROM account WHERE username='{username}'"
		get[1].execute(find_user)
		if get[1].fetchone():
			return "user are exist"
		else:
			input_data = f"""INSERT INTO account (name, type, total_downloads, books_upload, bio, username, password, profile_image, machine_id, register_date)
							 VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s)"""
			val = (username, 'user', '0', '0', "No bio", username, password, "", machine_id, self.now)
			try:
				get[1].execute(input_data, val)
				get[0].commit()
				get[0].close()
				return "Success"
			except Exception as e:
				print(e)
				return 'Failed'

	def login_via_machine_id(self, machine_id):
		get = self.getter.connect()
		find_machine = f"SELECT * FROM account WHERE machine_id='{machine_id}'"
		get[1].execute(find_machine)
		info = get[1].fetchone()
		if info != None:
			data = {'status':'Success','name':info[0], 'type':info[1], 'total_downloads':info[2],
					'books_upload':self.books.get_total_books_upload_byusername(info[5]), 'bio':info[4], 'username':info[5], 'password':info[6],
					'profile_image':info[7], 'machine_id':info[8], 'register_date':info[9]}
			return data
		else:
			return "Failed"


	def update_profile(self, name, bio, img_url, password, username):
		get = self.getter.connect()
		query = f"UPDATE account SET name='{name}', bio='{bio}', profile_image='{img_url}', password='{password}' WHERE username='{username}'"
		try:
			get[1].execute(query)
			get[0].commit()
			get[0].close()
			return True
		except Exception as e:
			print(e)
			return False
