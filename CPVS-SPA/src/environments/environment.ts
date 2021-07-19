// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.
const SYSTEM_CODE = 5;
export const environment = {
  production: false,
  systemCode: SYSTEM_CODE,

  apiUrlEC: 'http://10.4.5.132:1112/api/',
  api: 'http://10.4.5.132:1112/api/',

  apiDoc: 'http://10.4.5.132:1112:1004',
  apiBuilding: 'http://10.4.5.174:107/api/',
  apiUrl: 'http://10.4.5.174:106/api/',
  apiUrl2: 'http://10.4.5.174:106/api/',
  hub: 'http://10.4.5.132:1112/ec-hub',
  // hubCPVS: 'http://10.4.4.228:11111/scalingHub',
  hubCPVS: 'http://10.4.5.132:1112/ess-hub',
};
