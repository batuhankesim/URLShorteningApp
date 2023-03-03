# URLShorteningApp

Simple URL shortening Service.

## Features

- Microsoft.EntityFrameworkCore.InMemory is used in this project, so the added records are stored in memory as long as the API is running.
- One API url is used for url shortening and the other API url is used to get the original url
- The project checks that the request to shorten the url is valid, if it is invalid, it returns this information.
- If a valid url request is received, it stores the shortened url information in memory. In this way, you can get the original version of the shortened url again.

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

## POST URL to take a short URL and redirect to the original URL.

`http://localhost:<port>/api/Redirect`

Example request to redirect shortened uri address


```json 
 {
    "ShortUrl": "https//sample-site/XLNSoy/"
 }
```
