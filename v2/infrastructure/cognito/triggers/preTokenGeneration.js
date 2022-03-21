exports.handler = async (event) => {
  const scope = event.request.groupConfiguration.groupsToOverride
    .map((item) => `${item}-${event.callerContext.clientId}`)
    .join(" ");

  event.response = {
    claimsOverrideDetails: {
      claimsToAddOrOverride: {
        scope,
      },
    },
  };

  return event;
};
