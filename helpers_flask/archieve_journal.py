import requests
import os
import sys
from flask import Flask, jsonify
from flask_restful import Resource, Api, reqparse
from flask import Flask, render_template, request
from flask_uploads import UploadSet, configure_uploads, IMAGES
import uuid
from flask_cors import CORS
from werkzeug.utils import secure_filename

import helpers.search_arxiv as search_arxiv


class this_archieve_journal(Resource):
	def __init__(self):
		self._journal = search_arxiv.thisSearch()


	def get_all_journal(self, page):
		page = int(page)
		result = self._journal.grab(page)
		return jsonify({"total_result":result[1],"data":result[0]})

	def get(self):
		id = request.args.get('id')
		if id == 'all_journal':
			page = request.args.get('page')
			return self.get_all_journal(page)