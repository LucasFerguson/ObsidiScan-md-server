using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class MarkdownFile
{
    public string id;
    public string title;
    public string path;
    public string content;
}

public class MarkdownManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI noteDisplay;

    [Header("API Configuration")]
    public string apiBaseUrl = "https://notes.lucaslad.com/";

    private Dictionary<string, MarkdownFile> markdownFiles = new();

    void Awake()
    {
        markdownFiles = new Dictionary<string, MarkdownFile>
        {
            {
                "sa8A4",
                new MarkdownFile
                {
                    id = "sa8A4",
                    filename = "Example note 1",
                    meta = new MarkdownMeta
                    {
                        dateCreated = "2025-05-03T12:12:32",
                        dateModified = "2025-05-03T12:36:36",
                        lastEditedBy = "laptop",
                        title = "Example note 1",
                        aliases = "",
                        tags = ""
                    },
                    body = "This is the body of Example note 1.\nIt spans multiple lines."
                }
            },
            {
                "bX91Z",
                new MarkdownFile
                {
                    id = "bX91Z",
                    filename = "Weekly Report",
                    meta = new MarkdownMeta
                    {
                        dateCreated = "2025-05-01T09:00:00",
                        dateModified = "2025-05-01T17:00:00",
                        lastEditedBy = "desktop",
                        title = "Weekly Report",
                        aliases = "weekly",
                        tags = "report,log"
                    },
                    body = "## Weekly Summary\n\n- Completed milestone 1\n- Prepared for client demo"
                }
            },
            {
                "A2c9p",
                new MarkdownFile
                {
                    id = "A2c9p",
                    filename = "Meeting Notes",
                    meta = new MarkdownMeta
                    {
                        dateCreated = "2025-04-28T15:22:10",
                        dateModified = "2025-04-28T15:40:45",
                        lastEditedBy = "tablet",
                        title = "Meeting Notes - April 28",
                        aliases = "mtg,april28",
                        tags = "meetings"
                    },
                    body = "- Discussed project timeline\n- Action item: Review requirements"
                }
            },
            {
                "Zk19X",
                new MarkdownFile
                {
                    id = "Zk19X",
                    filename = "Todo List",
                    meta = new MarkdownMeta
                    {
                        dateCreated = "2025-05-03T08:00:00",
                        dateModified = "2025-05-03T08:10:00",
                        lastEditedBy = "phone",
                        title = "Todo List",
                        aliases = "",
                        tags = "tasks"
                    },
                    body = "- [ ] Buy groceries\n- [x] Fix the Unity script\n- [ ] Email team"
                }
            },
            {
                "Qw3rT",
                new MarkdownFile
                {
                    id = "Qw3rT",
                    filename = "Laptop System Summary",
                    meta = new MarkdownMeta
                    {
                        dateCreated = "2025-05-02T10:00:00",
                        dateModified = "2025-05-02T11:00:00",
                        lastEditedBy = "desktop",
                        title = "Laptop System Summary",
                        aliases = "",
                        tags = ""
                    },
                    body = "# Lucas' Laptop Computer System Summary:\n**System Model**: HP ENVY x360 Convertible 15m-ee0xxx\n**OS Name**: Microsoft Windows 10\n**CPU**: AMD Ryzen 5 4500U with Radeon Graphics\n**RAM**: 8.00 GB\n**SSD**: 256 GB"
                }
            },
            {
                "XyZ12",
                new MarkdownFile
                {
                    id = "XyZ12",
                    filename = "Desktop System Summary",
                    meta = new MarkdownMeta
                    {
                        dateCreated = "2025-05-02T14:00:00",
                        dateModified = "2025-05-02T15:00:00",
                        lastEditedBy = "desktop",
                        title = "Desktop System Summary",
                        aliases = "",
                        tags = "planning,project"
                    },
                    body = "Lucas' Desktop System Summary:\n**OS Name**: Microsoft Windows 10\n**CPU**: AMD Ryzen 5 4500U with Radeon Graphics\n**RAM**: 8.00 GB\n**GPU**: NVIDIA GeForce GTX 1060 with 6GB of VRAM\n**SSD**: 447 GB and 954 GB\n**History**: I originally purchased my desktop from IBUYPOWER in 2017 after saving all the money I got from birthdays, Christmas, and doing chores for about 4 years."
                }
            },
            {
                "SwA01",
                new MarkdownFile
                {
                    id = "SwA01",
                    filename = "Main Switch Rack 1",
                    meta = new MarkdownMeta
                    {
                        dateCreated = "2025-04-28T09:30:00",
                        dateModified = "2025-04-28T09:45:00",
                        lastEditedBy = "network_team",
                        title = "Main Switch Rack 1",
                        aliases = "",
                        tags = "networking,switch,datacenter"
                    },
                    body = "Main Switch Rack 1 (Data Center):\n**Device**: Cisco Catalyst 9300 Series\n**Location**: Server Room A, Rack 1\n**Ports**: 48 x 1GbE, 4 x 10GbE uplinks\n**History**: Installed by the IT Networking Team (lead: Priya Shah) during the 2023 infrastructure upgrade. All core office network traffic is routed through this switch."
                }
            },
            {
                "ApC12",
                new MarkdownFile
                {
                    id = "ApC12",
                    filename = "Conference Room Access Point",
                    meta = new MarkdownMeta
                    {
                        dateCreated = "2025-05-01T13:15:00",
                        dateModified = "2025-05-01T13:20:00",
                        lastEditedBy = "wireless_admin",
                        title = "Conference Room Access Point",
                        aliases = "",
                        tags = "wifi,access-point,wireless"
                    },
                    body = "Conference Room Wireless Access Point:\n**Device**: Ubiquiti UniFi 6 Long-Range\n**Location**: Ceiling, Conference Room C\n**SSID**: OfficeGuest, OfficeSecure\n**History**: Installed by wireless admin Sarah Kim in April 2025 to provide high-density Wi-Fi coverage for meetings and presentations."
                }
            },
            {
                "PtB07",
                new MarkdownFile
                {
                    id = "PtB07",
                    filename = "Patch Panel B7",
                    meta = new MarkdownMeta
                    {
                        dateCreated = "2025-03-15T10:10:00",
                        dateModified = "2025-03-15T10:20:00",
                        lastEditedBy = "cabling_team",
                        title = "Patch Panel B7",
                        aliases = "",
                        tags = "patch-panel,cabling,networking"
                    },
                    body = "Patch Panel B7 (West Wing):\n**Type**: Cat6 24-Port Patch Panel\n**Location**: Network Closet B, Wall Rack\n**Connections**: Serves offices 201-218\n**History**: Installed by the cabling team led by Miguel Torres during the west wing expansion in March 2025. All cables neatly labeled and tested for gigabit speeds."
                }
            }

        };
    }

    void Start()
    {
        StartCoroutine(FetchAllNotes());
    }

    // Fetch metadata and content of all notes at startup
    IEnumerator FetchAllNotes()
    {
        using UnityWebRequest request = UnityWebRequest.Get(apiBaseUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching notes list: " + request.error);
            yield break;
        }

        MarkdownFile[] metadataList = JsonHelper.FromJson<MarkdownFile>(request.downloadHandler.text);

        foreach (var note in metadataList)
        {
            yield return StartCoroutine(FetchNoteContent(note.id));
        }

        // Optionally display first note
        if (metadataList.Length > 0)
        {
            DisplayNote(metadataList[0].id);
        }
    }

    // Fetch content of a single note and store it
    IEnumerator FetchNoteContent(string id)
    {
        string url = $"{apiBaseUrl}/{id}";
        using UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error fetching note '{id}': {request.error}");
            yield break;
        }

        MarkdownFile fullNote = JsonUtility.FromJson<MarkdownFile>(request.downloadHandler.text);
        markdownFiles[fullNote.id] = fullNote;
    }

    public MarkdownFile GetMarkdownById(string id)
    {
        if (markdownFiles.TryGetValue(id, out var file))
            return file;

        Debug.LogWarning($"Markdown file with ID '{id}' not found.");
        return null;
    }

    public void DisplayNote(string id)
    {
        var note = GetMarkdownById(id);
        if (note != null && noteDisplay != null)
        {
            noteDisplay.text = note.content;
        }
    }
}
