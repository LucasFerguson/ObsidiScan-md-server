import os
import json
import frontmatter
from datetime import datetime

def to_isoformat(timestamp):
    return datetime.fromtimestamp(timestamp).isoformat(timespec="minutes")

def slug_from_filename(filename):
    return os.path.splitext(os.path.basename(filename))[0]

def index_notes(vault_path="./obsidian vault example"):
    notes_index = {}

    for root, _, files in os.walk(vault_path):
        for filename in files:
            if filename.endswith(".md"):
                file_path = os.path.join(root, filename)

                try:
                    with open(file_path, "r", encoding="utf-8") as f:
                        post = frontmatter.load(f)

                    file_stats = os.stat(file_path)
                    date_created = to_isoformat(file_stats.st_ctime)
                    date_modified = to_isoformat(file_stats.st_mtime)

                    note_id = post.get("id") or slug_from_filename(filename)
                    title = post.get("title") or slug_from_filename(filename)
                    aliases = post.get("aliases", "")
                    tags = post.get("tags", "")

                    with open(file_path, "r", encoding="utf-8") as f:
                        full_text = f.read()

                    # Remove frontmatter for body
                    body = full_text.split("---\n", 2)[-1].strip()

                    notes_index[note_id] = {
                        "id": note_id,
                        "filename": title,
                        "meta": {
                            "dateCreated": date_created,
                            "dateModified": date_modified,
                            "lastEditedBy": post.get("lastEditedBy", ""),
                            "title": title,
                            "aliases": aliases,
                            "tags": tags
                        },
                        "body": body
                    }

                except Exception as e:
                    print(f"Failed to process {file_path}: {e}")

    output_path = os.path.join(os.getcwd(), "notes_index.json")
    with open(output_path, "w", encoding="utf-8") as out_file:
        json.dump(notes_index, out_file, indent=2)

    print(f"Index saved to {output_path}")

if __name__ == "__main__":
    index_notes()
