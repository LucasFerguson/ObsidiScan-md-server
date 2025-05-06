import os
import json
import frontmatter
from datetime import datetime

def to_isoformat(timestamp):
    """Convert Unix timestamp to ISO 8601 string (YYYY-MM-DDTHH:MM)."""
    return datetime.fromtimestamp(timestamp).isoformat(timespec="minutes")

def ensure_str_date(date_val, fallback_ts):
    """Ensure the date is in ISO 8601 string format."""
    if isinstance(date_val, datetime):
        return date_val.isoformat(timespec="minutes")
    if isinstance(date_val, str):
        return date_val
    return to_isoformat(fallback_ts)

def slug_from_filename(filename):
    """Get filename without extension."""
    return os.path.splitext(os.path.basename(filename))[0]

def extract_body(content):
    """Remove frontmatter and return just the body."""
    parts = content.split('---\n')
    return parts[-1].strip() if len(parts) >= 3 else content.strip()

def index_notes(vault_path="./obsidian vault example"):
    notes_list = []

    for root, _, files in os.walk(vault_path):
        for filename in files:
            if filename.endswith(".md"):
                file_path = os.path.join(root, filename)

                try:
                    with open(file_path, "r", encoding="utf-8") as f:
                        post = frontmatter.load(f)

                    file_stats = os.stat(file_path)

                    # Extract metadata with fallbacks
                    note_id = post.get("id") or slug_from_filename(filename)
                    title = post.get("title") or slug_from_filename(filename)
                    aliases = post.get("aliases", "")
                    tags = post.get("tags", "")
                    last_edited_by = post.get("lastEditedBy", "")

                    date_created = ensure_str_date(post.get("dateCreated"), file_stats.st_ctime)
                    date_modified = ensure_str_date(post.get("dateModified"), file_stats.st_mtime)

                    with open(file_path, "r", encoding="utf-8") as f:
                        full_text = f.read()

                    note = {
                        "id": note_id,
                        "filename": title,
                        "meta": {
                            "dateCreated": date_created,
                            "dateModified": date_modified,
                            "lastEditedBy": last_edited_by,
                            "title": title,
                            "aliases": aliases,
                            "tags": tags
                        },
                        "body": extract_body(full_text)
                    }

                    notes_list.append(note)

                except Exception as e:
                    print(f"⚠️ Failed to process {file_path}: {e}")

    output_path = os.path.join(os.getcwd(), "notes_index.json")
    with open(output_path, "w", encoding="utf-8") as out_file:
        json.dump(notes_list, out_file, indent=2, ensure_ascii=False)

    print(f"✅ Indexed {len(notes_list)} notes to {output_path}")

if __name__ == "__main__":
    index_notes()
