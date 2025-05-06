# ObsidiScan-md-server
Provides notes to the Meta Quest Unity App
`server.py`

# Setup
Create Python venv
```bash
python -m venv .venv
.venv\Scripts\activate.bat
```

Install requirements flask and python-frontmatter
```bash
pip install flask
pip install python-frontmatter
```

## Start server
```bash
python server.py
```

## Test server
1. Get a single markdown file by ID
```bash
curl http://localhost:5000/api/markdown/sa8A4
```
2. Get all markdown files
```bash
curl http://localhost:5000/api/markdown
```
3. Test get a non-existent file
```bash
curl http://localhost:5000/api/markdown/doesNotExist
```
4. Save response to a file
```bash
curl http://localhost:5000/api/markdown/sa8A4 -o output.json
```