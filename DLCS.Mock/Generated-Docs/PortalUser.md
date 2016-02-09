
# PortalUser

A user of the portal

```javascript
/customers/{0}/portalUsers/{1}
```


## Supported properties


### email

The email address


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:PortalUser|xsd:string|False|False|


### created

Create date


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:PortalUser|xsd:dateTime|False|False|


### role

List of Role URIs that the user has


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:PortalUser|xsd:string|False|False|


### enabled

Whether the user can log in


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:PortalUser|xsd:boolean|False|False|


## Supported operations

**Template: **```javascript
/customers/{0}/portalUsers/{1}
```


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieve a Portal User||vocab:PortalUser||
|PUT|create or replace a Portal User|vocab:PortalUser|vocab:PortalUser||
|PATCH|Update the supplied fields of the Portal User|vocab:PortalUser|vocab:PortalUser|200 patched Portal User|
|DELETE|Delete the Portal User||owl:Nothing||

