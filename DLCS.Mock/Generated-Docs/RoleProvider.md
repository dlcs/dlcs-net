
# RoleProvider

Resource that represents the means by which the DLCS acquires roles to enforce an access control session. The DLCS maintains the session, but needs an external auth service (CAS, OAuth etc) to authenitcate the user and acquire roles. Credentials are stored in S3 and not returned via the API.

```javascript
/customers/{0}/authServices/{1}/roleProvider
```


## Supported properties


### configuration

JSON configuration blob for this particular service


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:RoleProvider|xsd:string|False|False|


### credentials

Credentials - not exposed via API, but can be written to by customer.


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:RoleProvider|xsd:string|False|True|


## Supported operations

**Template: **```javascript
/customers/{0}/authServices/{1}/roleProvider
```


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieve a Role Provider||vocab:RoleProvider||
|PUT|create or replace a Role Provider|vocab:RoleProvider|vocab:RoleProvider||
|PATCH|Update the supplied fields of the Role Provider|vocab:RoleProvider|vocab:RoleProvider|200 patched Role Provider|
|DELETE|Delete the Role Provider||owl:Nothing||

