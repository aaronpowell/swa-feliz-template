# Find the Dockerfile for mcr.microsoft.com/azure-functions/python:3.0-python3.8-core-tools at this URL
# https://github.com/Azure/azure-functions-docker/blob/dev/host/3.0/buster/amd64/python/python38/python38-core-tools.Dockerfile
FROM mcr.microsoft.com/azure-functions/python:3.0-python3.8-core-tools

# Copy library scripts to execute
COPY library-scripts/*.sh library-scripts/*.env /tmp/library-scripts/

# Install Node.js and Azure Static Web Apps CLI
ARG NODE_VERSION="lts/*"
ENV NVM_DIR="/usr/local/share/nvm" \
    NVM_SYMLINK_CURRENT=true \
    PATH="${NVM_DIR}/current/bin:${PATH}"
RUN bash /tmp/library-scripts/node-debian.sh "${NVM_DIR}" "${NODE_VERSION}" "${USERNAME}" \
    && su vscode -c "umask 0002 && . /usr/local/share/nvm/nvm.sh && nvm install ${NODE_VERSION} 2>&1" \
    && su vscode -c "umask 0002 && npm install --cache /tmp/empty-cache -g @azure/static-web-apps-cli" \
    && apt-get clean -y && rm -rf /var/lib/apt/lists/*

# Needed for Ionide
RUN wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb; \
    sudo dpkg -i packages-microsoft-prod.deb; \
    sudo apt-get update && \
    sudo apt-get install -y dotnet-sdk-5.0

# Set default language to F#
ENV DOTNET_NEW_PREFERRED_LANG=F#

# Install the F# project templates for dotnet-new
RUN dotnet new --install Microsoft.Azure.WebJobs.ItemTemplates \
    && dotnet new --install Microsoft.Azure.WebJobs.ProjectTemplates