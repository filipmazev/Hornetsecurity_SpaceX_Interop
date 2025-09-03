export class RedditResponse {
    campaignUrl?: string;
    launchUrl?: string;
    mediaUrl?: string;
    recoveryUrl?: string;

    constructor(
        campaignUrl: string | undefined,
        launchUrl: string | undefined,
        mediaUrl: string | undefined,
        recoveryUrl: string | undefined
    ) {
        this.campaignUrl = campaignUrl;
        this.launchUrl = launchUrl;
        this.mediaUrl = mediaUrl;
        this.recoveryUrl = recoveryUrl;
    }
}