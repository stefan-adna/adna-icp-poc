# asset_cannister

This is using the default [ICP AssetCanister](https://internetcomputer.org/docs/current/references/asset-canister) which is defined in the `dfx.json` file `"type": "assets"`

Make sure you deploy with the proper identity using the `--identity` flag

Create new identity
```
dfx identity new myidentity
```

Deploy using the created identity
```
dfx deploy --playground --identity myidentity
```

In the client app you need the provide the private key `*.pem` file to successfully upload files. To create the `*.pem` use the following command
```
dfx identity export myidentity >myidentity.pem
```