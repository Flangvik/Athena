+++
title = "HTTPX"
chapter = false
weight = 103
+++

## Summary
Advanced HTTP profile with malleable configuration support and message transforms for enhanced OPSEC. Based on the httpx C2 profile with extensive customization options.

The HTTPX profile provides advanced obfuscation capabilities through malleable profiles, supporting multiple message transform techniques and flexible message placement strategies.

### Profile Options

#### Callback Domains
Array of callback domains to communicate with. Supports multiple domains for redundancy and domain rotation.

**Example:** `https://example.com:443,https://backup.com:443`

**Parameter Type:** Array

#### Domain Rotation
Domain rotation pattern for handling multiple callback domains:

- **fail-over**: Uses each domain in order until communication fails, then moves to the next
- **round-robin**: Cycles through domains for each request
- **random**: Randomly selects a domain for each request

**Default:** fail-over

#### Failover Threshold
Number of consecutive failures before switching to the next domain in fail-over mode.

**Default:** 5

#### Callback Interval in seconds
Time to sleep between agent check-ins.

**Default:** 10

#### Callback Jitter in percent
Randomize the callback interval within the specified threshold.

**Default:** 23

#### Encrypted Exchange Check
**Required:** Must be true. The HTTPX profile uses RSA-4096 key exchange (EKE) for secure communication and cannot operate without it. This ensures all traffic is encrypted with client-side generated keys.

**Default:** true (Cannot be disabled)

#### Kill Date
The date at which the agent will stop calling back.

**Default:** 365 days from build

#### Raw C2 Config
JSON configuration file defining malleable profile behavior. This is a **REQUIRED** parameter that defines:

- HTTP method variations (GET, POST, PUT, DELETE, PATCH, OPTIONS, HEAD)
- Message placement strategies (query, cookie, header, body)
- Transform chains for obfuscation
- Custom headers and parameters

**Example config structure:**
```json
{
  "name": "default",
  "get": {
    "verb": "GET",
    "uris": ["/api/v1/data"],
    "client": {
      "headers": {},
      "parameters": {},
      "message": {
        "location": "query",
        "name": "id"
      },
      "transforms": [
        {"action": "base64url"},
        {"action": "prepend", "value": "pre="}
      ]
    },
    "server": {
      "headers": {},
      "transforms": [
        {"action": "prepend", "value": "pre="},
        {"action": "base64url"}
      ]
    }
  },
  "post": {
    "verb": "POST",
    "uris": ["/api/v1/submit"],
    "client": {
      "headers": {"Content-Type": "application/json"},
      "message": {
        "location": "body",
        "name": ""
      },
      "transforms": [
        {"action": "base64"},
        {"action": "append", "value": "post="}
      ]
    },
    "server": {
      "headers": {},
      "transforms": [
        {"action": "append", "value": "post="},
        {"action": "base64"}
      ]
    }
  }
}
```

### Proxy Configuration

#### proxy_host
Proxy server hostname or IP address for outbound connections.

**Example:** `proxy.company.com`

#### proxy_port
Proxy server port number.

**Example:** `8080`

#### proxy_user
Username for proxy authentication (if required).

#### proxy_pass
Password for proxy authentication (if required).

### Domain Fronting

#### domain_front
Domain fronting header value. Sets the `Host` header to this value for traffic obfuscation.

**Example:** `cdn.example.com`

### Request Timeout

#### timeout
Request timeout in seconds.

**Default:** 240

### Transform Actions

The HTTPX profile supports the following transform actions:

- **base64**: Base64 encoding
- **base64url**: URL-safe Base64 encoding
- **netbios**: NetBIOS encoding (lowercase)
- **netbiosu**: NetBIOS encoding (uppercase)
- **xor**: XOR encryption with key
- **prepend**: Prepend data with a value
- **append**: Append data with a value

### Message Placement

The HTTPX profile supports multiple message placement strategies:

- **query**: Place message in URL query parameter
- **cookie**: Place message in HTTP cookie
- **header**: Place message in custom HTTP header
- **body**: Place message in HTTP request body

### Usage Notes

1. The HTTPX profile requires a `raw_c2_config` parameter with a valid JSON configuration.
2. Both GET and POST variations must be defined in the configuration.
3. Transform chains are applied in order on the client side and in reverse order on the server side.
4. Domain rotation can help avoid detection by changing callback domains.
5. The profile supports all standard HTTP methods for maximum flexibility.

