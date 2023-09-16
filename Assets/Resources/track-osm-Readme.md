## track-osm.csv
The file contains mapping from track ids (from status update to osm way ids)
using
https://api.openstreetmap.org/api/0.6/way/548808641.json
the way can be split into nodes. Each nodes has coordinates which can be retrived using this call:
https://api.openstreetmap.org/api/0.6/node/2199945016.json
## How to Find way id 
Get next id from sample json. Locate object (by name) in the pdf files:
* 42601-101-05.pdf
* 42601-101-04.pdf
* 42601-101-03.pdf
* 42601-101-02.pdf
* 42601-101-01.pdf

Than find closes object in osm (near this https://www.openstreetmap.org/query?lat=46.31328&lon=7.96859) right mous click next to desired object ("query features") a list with objects will be shown. Mouse hooveer highlight object in map to validate if it is the correct object. When correct object is found use id.