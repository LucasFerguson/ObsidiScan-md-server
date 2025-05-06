from flask import Flask, jsonify
from datetime import datetime

app = Flask(__name__)

# Mock markdown data
markdown_files = {
    "sa8A4": {
        "id": "sa8A4",
        "filename": "Example note 1",
        "meta": {
            "dateCreated": "2025-05-03T12:12:32",
            "dateModified": "2025-05-03T12:36:36",
            "lastEditedBy": "laptop",
            "title": "Example note 1",
            "aliases": "",
            "tags": ""
        },
        "body": "## Example note 1\n\nThis is some sample markdown.\n- List item 1\n- List item 2"
    },
    "b7Qx2": {
        "id": "b7Qx2",
        "filename": "VR Project Planning",
        "meta": {
            "dateCreated": "2025-04-12T08:15:00",
            "dateModified": "2025-04-15T09:22:10",
            "lastEditedBy": "desktop",
            "title": "VR Project Planning",
            "aliases": "",
            "tags": ""
        },
        "body": "# VR Project Planning\n\n- Set goals\n- Assign tasks\n- Timeline estimate"
    },
    "Yz93P": {
        "id": "Yz93P",
        "filename": "Milestone Reflection",
        "meta": {
            "dateCreated": "2025-05-01T18:00:00",
            "dateModified": "2025-05-01T18:05:00",
            "lastEditedBy": "tablet",
            "title": "Milestone Reflection",
            "aliases": "",
            "tags": ""
        },
        "body": "## Reflections\n\nMilestone 1 went well overall. Next steps:\n\n- Fix bugs\n- Improve scene transitions\n"
    }
}

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
    app.run(port=8080)
