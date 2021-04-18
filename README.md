# WebPerformancer 


This is console application, dedicated to estimating performance of particular web site.
Basic functions of this project:

- Parsing sitemap files from different locations
- Parsing HTML code for finding links on the page
- Assembling lists of available links on the web site
- Estimating the performance of each link
### Sitemap files
Application is heading **http://.../robots.txt** to find url of sitemap file of the web site. 
If there's no **robots.txt**, sitemap files are searched in following locations:
- **.../sitemap.xml**
- **.../sitemap_index.xml**
- **.../sitemap**
- **.../sitemap_index**
 
The **sitemap.xml**  can contain links to several distributed sitemap files, in this case application is using recursive approach to record all links in found files. 
### Parsing code
In order to find links on given web page, application is reading HTML code of page and find all links, which are in the same domain.
### Performance estimating
After reaching all the sitemap documents and parsing code of web page, application gives you option to query all urls and present it in sorted structure. 

