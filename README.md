[//]: # (---- LOGO ----------------------------------------------------------- )

<div style="display:flex; align-items:center; padding:0 0 20px 0;">
	<img
		src="https://i.imgur.com/Kt64d3S.png"
		width="100"
		style="border-radius:50%"
		alt="demen-logo"
	/>
</div>

[// ]: #header (---- header -------------------------------------------------- )

<div id="header" >
	<img
		alt="GitHub release (latest by date)"
		src="https://img.shields.io/github/v/release/hbenvenutti/Demen.Net?style=plastic"
		title="Latest Release"
	/>
	<img
		alt="GitHub last commit (branch)"
		src="https://img.shields.io/github/last-commit/hbenvenutti/Demen.Net/feature?label=last%20commit&style=plastic"
		title="Last Commit on Feature branch"
	/>
	<img
		alt="GitHub contributors"
		src="https://img.shields.io/github/contributors/hbenvenutti/Demen.Net?style=plastic"
		title="Contributors"
	>
</div>

<div>
	<img
		alt="GitHub commit activity (feature)"
		src="https://img.shields.io/github/commit-activity/w/hbenvenutti/Demen.Net/feature?style=plastic"
	>
	<img
		alt="GitHub forks"
		src="https://img.shields.io/github/forks/hbenvenutti/Demen.Net?style=plastic"
	>
	<img
		alt="GitHub Repo stars"
		src="https://img.shields.io/github/stars/hbenvenutti/Demen.Net?style=plastic"
	>
	<img
		alt="GitHub watchers"
		src="https://img.shields.io/github/watchers/hbenvenutti/Demen.Net?style=plastic"
	>
	<img
		alt="GitHub pull requests"
		src="https://img.shields.io/github/issues-pr/hbenvenutti/Demen.Net?style=plastic"
	>
	<img
		alt="GitHub closed pull requests"
		src="https://img.shields.io/github/issues-pr-closed/hbenvenutti/Demen.Net?style=plastic"
	>
	<img
		alt="GitHub code size in bytes"
		src="https://img.shields.io/github/languages/code-size/hbenvenutti/Demen.Net?style=plastic"
	>
</div>

<div align="center" style="display:flex; padding:15px 0; justify-content: space-between">
	<img
		height="60"
		src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-plain.svg"
		alt="csharp"
	/>
	<img
		height="60"
		src="https://upload.wikimedia.org/wikipedia/commons/thumb/7/7d/Microsoft_.NET_logo.svg/1024px-Microsoft_.NET_logo.svg.png"
		alt="dotnet"
	>
	 <img
		height="60"
		src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/docker/docker-plain-wordmark.svg"
		alt="docker"
	/>
	<img
		height="60"
		src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/postgresql/postgresql-plain-wordmark.svg"
		alt="postgresql"
	/>
</div>



# Demen Backend

## Getting Started
**Commands:**

- Run: `❯ dotnet run --project API/API.csproj`
- Build: `❯ dotnet build Demen.sln`
- Test: `❯ dotnet test Demen.sln`
- Add Migration:
	```bash
	dotnet ef migrations add \
	--project Data/Data.csproj \
	--startup-project API/API.csproj \
	--context Demen.Data.Contexts.DemenContext \
	--configuration Debug \
	--framework net7.0 \
	--output-dir Migrations \
	<Migration Name>
	```
- Remove Migration:
	```bash
	dotnet ef migrations remove \
	--project Data/Data.csproj \
	--startup-project API/API.csproj \
	--context Demen.Data.Contexts.DemenContext \
	--configuration Debug \
	--framework net7.0 \
	--force
	```

**Requirements:**

- .NET Framework 7.0
- Local Database
  - Database: PostgreSQL:
  - User: admin;
  - Password: 1234;

### Docker Setup
