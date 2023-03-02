# URLShorteningApp

Simple URL shortening Service.


## POST URL to generate shortened uri

`http://localhost:<port>/api/Url`

- The input URL format must be valid.
- Maximum character length for the hash portion of the URL is 6.

Sample request for automatic shortening

```json 
 {
    "url": "https://www.sample-site.com/karriere/berufserfahrene/direkteinstieg/",
    "IsAutoGenerate": true
 }
```


Example request to manually generate the shortened uri address

```json 
 {
    "url": "https://www.sample-site.com/karriere/berufserfahrene/direkteinstieg/",
    "ShortUrl": "https//sample-site/XLNSoy/"
 }
```

## POST URL to ake a short URL and redirect to the original URL.

`http://localhost:<port>/api/Redirect`

Example request to redirect shortened uri address


```json 
 {
    "ShortUrl": "https//sample-site/XLNSoy/"
 }
```
