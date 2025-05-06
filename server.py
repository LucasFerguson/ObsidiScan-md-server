from flask import Flask, jsonify
from datetime import datetime
import json

app = Flask(__name__)

with open('notes.json', 'r') as f:
    markdown_files = json.load(f)

@app.route("/api/markdown/<file_id>", methods=["GET"])
def get_markdown_file(file_id):
    file = markdown_files.get(file_id)
    if file:
        return jsonify(file)
    return jsonify({"error": "File not found"}), 404

@app.route("/api/markdown", methods=["GET"])
def get_all_files():
    return jsonify(list(markdown_files.values()))

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=8080)
