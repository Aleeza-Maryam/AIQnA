# ── Base Image ──────────────────────────────────

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# ── Working Directory ────────────────────────────

WORKDIR /app

# ── Copy Project File ────────────────────────────

COPY AIQnA.csproj .

# ── Restore Packages ─────────────────────────────

RUN dotnet restore

# ── Copy All Files ───────────────────────────────

COPY . .

# ── Build ────────────────────────────────────────

RUN dotnet publish -c Release -o /app/publish

# ── Runtime Image ────────────────────────────────

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS runtime

WORKDIR /app

# Build se sirf compiled files copy karo
COPY --from=build /app/publish .

# ── Run Command ──────────────────────────────────

CMD ["dotnet", "AIQnA.dll"]
