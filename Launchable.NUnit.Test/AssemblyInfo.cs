// This is how a customer "installs" Launchable subsetting
// That it needs a source code change is rather intrusive,
// ideally there should be a way to do this via Engine Extension
// https://docs.nunit.org/articles/nunit-engine/extensions/Installing-Extensions.html
// but I don't see how
[assembly: Launchable.NUnit.Launchable]
