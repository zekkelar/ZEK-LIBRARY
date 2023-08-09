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

import helpers.searchFuzz as searchFuzz
import helpers.search_arxiv as search_arxiv
import helpers.search_texts as search_texts
import helpers.controller as keys
url = keys.URL


class this_search(Resource):
	def __init__(self):
		self._search2 = search_texts.this_search_texts()
		self._searchFuzz = searchFuzz.this_search_fuzz()
		self._search1 = search_arxiv.this_search_arxiv()

	def searchExternal(self, query, page):
		gett = self._search1.grab(page, query)
		if gett != False:
			return jsonify({"total_result":gett[1],"data":gett[0]})
		else:
			return jsonify({"total_result":"not found"})

	def searchInternal(self, query):
		gett = self._searchFuzz.fsm(query)
		if gett != False:
			return jsonify({'data':gett})
		else:
			return jsonify({'data':'not found'})

	def searchExternal2(self, query, page):
		gett = self._search2.grab(page, query)
		if gett != False:
			return jsonify({"total_result":gett[1],"data":gett[0]})
		else:
			return jsonify({"total_result":"not found"})

	def get(self):
		id = request.args.get('id')
		if id == 'searchExternal1':
			query = request.args.get('query')
			page = request.args.get('page')
			return self.searchExternal(query, page)

		if id == 'searchExternal2':
			query = request.args.get('query')
			page = request.args.get('page')
			return self.searchExternal2(query, page)

		if id == 'searchInternal':
			query = request.args.get('query')
			return self.searchInternal(query)
